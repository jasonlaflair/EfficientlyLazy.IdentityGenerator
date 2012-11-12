using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using EfficientlyLazy.IdentityGenerator.Entity;

namespace EfficientlyLazy.IdentityGenerator
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Generator : IGenerator
    {
        internal INameData NameData { get; private set; }
        internal IAddressData AddressData { get; private set; }
        internal IEnumerable<ISSNAreaCodeData> SSNAreaCodeData { get; private set; }

        private readonly RandomEngine _random = new RandomEngine();

        #region DEFAULTS
        private const GenderFilter DEFAULT_GENDER_FILTER = GenderFilter.Both;
        private const int DEFAULT_MINIMUM_AGE = 1;
        private const int DEFAULT_MAXIMUM_AGE = 100;
        #endregion

        /// <summary>
        /// Configures this instance.
        /// </summary>
        /// <returns></returns>
        public static IGeneratorConfiguration Configure()
        {
            return new GeneratorOptions();
        }

        internal class GeneratorOptions : IGeneratorConfiguration, IGeneratorOptions
        {
            public bool NameIncluded { get; set; }
            public bool SSNIncluded { get; set; }
            public bool SSNDashed { get; set; }
            public bool DOBIncluded { get; set; }
            public bool AddressInclude { get; set; }
            public GenderFilter GenderFilter { get; set; }

            public int MinimumAge { get; set; }
            public int MaximumAge { get; set; }

            public INameData ExternalNameData { get; set; }
            public IAddressData ExternalAddressData { get; set; }
            public IEnumerable<ISSNAreaCodeData> ExternalSSNAreaCodeData { get; set; }

            public GeneratorOptions()
            {
                GenderFilter = DEFAULT_GENDER_FILTER;
                MinimumAge = DEFAULT_MINIMUM_AGE;
                MaximumAge = DEFAULT_MAXIMUM_AGE;
            }

            public IGeneratorConfiguration IncludeName()
            {
                NameIncluded = true;
                GenderFilter = GenderFilter.Both;
                ExternalNameData = null;

                return this;
            }

            public IGeneratorConfiguration IncludeName(GenderFilter filter)
            {
                NameIncluded = true;
                GenderFilter = filter;
                ExternalNameData = null;

                return this;
            }

            public IGeneratorConfiguration IncludeName(INameData nameData)
            {
                NameIncluded = true;
                GenderFilter = nameData.GenderFilter;
                ExternalNameData = nameData;

                return this;
            }

            public IGeneratorConfiguration IncludeSSN()
            {
                SSNIncluded = true;
                SSNDashed = false;
                ExternalSSNAreaCodeData = null;

                return this;
            }

            public IGeneratorConfiguration IncludeSSN(bool makeDashed)
            {
                SSNIncluded = true;
                SSNDashed = makeDashed;
                ExternalSSNAreaCodeData = null;

                return this;
            }

            public IGeneratorConfiguration IncludeSSN(IEnumerable<ISSNAreaCodeData> ssnAreaCodeData)
            {
                SSNIncluded = true;
                SSNDashed = false;
                ExternalSSNAreaCodeData = ssnAreaCodeData;

                return this;
            }

            public IGeneratorConfiguration IncludeSSN(IEnumerable<ISSNAreaCodeData> ssnAreaCodeData, bool makeDashed)
            {
                SSNIncluded = true;
                SSNDashed = makeDashed;
                ExternalSSNAreaCodeData = ssnAreaCodeData;

                return this;
            }

            public IGeneratorConfiguration IncludeDOB()
            {
                DOBIncluded = true;
                MinimumAge = DEFAULT_MINIMUM_AGE;
                MaximumAge = DEFAULT_MAXIMUM_AGE;

                return this;
            }

            public IGeneratorConfiguration IncludeDOB(int minimum, int maximum)
            {
                DOBIncluded = true;
                MinimumAge = minimum;
                MaximumAge = maximum;

                return this;
            }

            public IGeneratorConfiguration IncludeAddress()
            {
                AddressInclude = true;
                ExternalAddressData = null;

                return this;
            }

            public IGeneratorConfiguration IncludeAddress(IAddressData addressData)
            {
                AddressInclude = true;
                ExternalAddressData = addressData;

                return this;
            }

            public IGenerator Build()
            {
                return new Generator(this);
            }
        }

        internal Generator(IGeneratorOptions options)
        {
            IncludeName = options.NameIncluded;
            IncludeAddress = options.AddressInclude;
            IncludeDOB = options.DOBIncluded;
            Genders = options.GenderFilter;
            IncludeSSN = options.SSNIncluded;
            IncludeSSNDashes = options.SSNDashed;
            MaximumAge = options.MaximumAge;
            MinimumAge = options.MinimumAge;

            if (IncludeName && options.ExternalNameData == null)
            {
                LoadInternalNameData(Genders);
            }
            else if (IncludeName)
            {
                LoadExternalNameData(options.ExternalNameData);
            }

            if (IncludeAddress && options.ExternalAddressData == null)
            {
                LoadInternalAddressData();
            }
            else if (IncludeAddress)
            {
                LoadExternalAddressData(options.ExternalAddressData);
            }

            if (IncludeSSN && options.ExternalSSNAreaCodeData == null)
            {
                LoadInternalSSNAreaCodeData();
            }
            else if (IncludeSSN)
            {
                LoadExternalSSNAreaCodeData(options.ExternalSSNAreaCodeData);
            }
        }

        /// <summary>
        /// Includes Name (first, middle, last) in identity generation
        /// </summary>
        /// <value>
        /// <c>true</c> if [include name]; otherwise, <c>false</c>. (default: false)
        /// </value>
        public bool IncludeName { get; private set; }

        /// <summary>
        /// Includes SSN in identity generation (default: false)
        /// </summary>
        /// <value>
        /// <c>true</c> if [include SSN]; otherwise, <c>false</c>. (default: false)
        /// </value>
        public bool IncludeSSN { get; private set; }

        /// <summary>
        /// Gets a value indicating whether [SSN dashed].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [SSN dashed]; otherwise, <c>false</c>. (default: false)
        /// </value>
        public bool IncludeSSNDashes { get; private set; }

        /// <summary>
        /// Includes Date Of Birth in identity generation (default: false)
        /// </summary>
        /// <value>
        /// <c>true</c> if [include DOB]; otherwise, <c>false</c>. (default: false)
        /// </value>
        public bool IncludeDOB { get; private set; }

        /// <summary>
        /// Includes Address (address line, city, state, zipcode) in identity generation (default: false)
        /// </summary>
        /// <value>
        /// <c>true</c> if [include address]; otherwise, <c>false</c>. (default: false)
        /// </value>
        public bool IncludeAddress { get; private set; }

        /// <summary>
        /// Includes Male names in identity generation (default: Both)
        /// </summary>
        public GenderFilter Genders { get; private set; }

        /// <summary>
        /// The minimum age use for random ages in identity generation (default: 1)
        /// </summary>
        /// <value>
        /// The minimum age. (default: 1)
        /// </value>
        public int MinimumAge { get; private set; }

        /// <summary>
        /// The maximum age use for random ages in identity generation (default: 100)
        /// </summary>
        /// <value>
        /// The maximum age. (default: 100)
        /// </value>
        public int MaximumAge { get; private set; }

        /// <summary>
        /// Generates a random <see cref="IName" />.
        /// </summary>
        /// <param name="filter">The <see cref="GenderFilter" />.</param>
        /// <returns>
        ///   <see cref="IName" />
        /// </returns>
        public IName GenerateName(GenderFilter filter)
        {
            IFirstNameData first;

            switch (filter)
            {
                case GenderFilter.Female:
                    first = NameData.FirstNameData.GetRandom(x => x.Gender == Gender.Female);
                    break;
                case GenderFilter.Male:
                    first = NameData.FirstNameData.GetRandom(x => x.Gender == Gender.Male);
                    break;
                case GenderFilter.Both:
                    first = NameData.FirstNameData.GetRandom();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("filter");
            }

            var middle = NameData.FirstNameData.GetRandom(x => x.Gender == first.Gender);
            var last = NameData.LastNameData.GetRandom();

            return new Name
                       {
                           First = first.Name,
                           Middle = middle.Name,
                           Last = last,
                           Gender = first.Gender
                       };
        }

        /// <summary>
        /// Generates an SSN.
        /// </summary>
        /// <returns>
        /// An SSN
        /// </returns>
        public string GenerateSSN()
        {
            return GenerateSSN(string.Empty, IncludeSSNDashes);
        }

        /// <summary>
        /// Generates an SSN.
        /// </summary>
        /// <param name="stateAbbreviation">The state the SSN should be based on.</param>
        /// <returns>
        /// An SSN
        /// </returns>
        public string GenerateSSN(string stateAbbreviation)
        {
            return GenerateSSN(stateAbbreviation, IncludeSSNDashes);
        }

        internal string GenerateSSN(string stateAbbreviation, bool includeDashes)
        {
            var ssn = string.Empty;

            var areaCode = SSNAreaCodeData.FirstOrDefault(x => x.StateAbbreviation == stateAbbreviation);

            if (areaCode != null)
            {
                var digits = _random.Next(areaCode.Minimum, areaCode.Maximum + 1);
                ssn = digits.ToString().PadLeft(3, '0');
            }

            do
            {
                var digit = _random.Next(0, 10);
                ssn += digit.ToString();
            }
            while (ssn.Length < 9);

            if (includeDashes)
            {
                ssn = Regex.Replace(ssn, @"(\d{3})(\d{2})(\d{4})", @"$1-$2-$3");
            }

            return ssn;
        }

        /// <summary>
        /// Generates a random <see cref="IAddress" />.
        /// </summary>
        /// <returns>
        ///   <see cref="IAddress" />
        /// </returns>
        public IAddress GenerateAddress()
        {
            var cityStateZip = AddressData.CityStateZips.GetRandom();

            var address = new Address
                              {
                                  City = cityStateZip.City,
                                  State = new State
                                              {
                                                  Abbreviation = cityStateZip.StateAbbreviation,
                                                  Name = cityStateZip.StateName
                                              },
                                  ZipCode = cityStateZip.ZipCode
                              };

            var streetType = AddressData.StreetTypes.GetRandom();
            var direction = AddressData.Directions.GetRandom();

            var streetNumber = _random.Next(1000, 10000);
            var street = _random.Next(1, 100);

            switch (street.ToString()[street.ToString().Length - 1])
            {
                case '1':
                    address.AddressLine = string.Format("{0} {1}st {2} {3}", streetNumber, street, streetType, direction);
                    break;
                case '2':
                    address.AddressLine = string.Format("{0} {1}nd {2} {3}", streetNumber, street, streetType, direction);
                    break;
                case '3':
                    address.AddressLine = string.Format("{0} {1}rd {2} {3}", streetNumber, street, streetType, direction);
                    break;
                default:
                    address.AddressLine = string.Format("{0} {1}th {2} {3}", streetNumber, street, streetType, direction);
                    break;
            }

            return address;
        }

        #region Date Of Birth

        private int _dobPossibilities;
        private int _dobClearThreshold;
        private readonly Hashtable _dobHistory = new Hashtable();

        /// <summary>
        /// Generates a random Date Of Birth.
        /// </summary>
        /// <returns>
        /// A Date Of Birth between the specified age ranges.
        /// </returns>
        public DateTime GenerateDOB()
        {
            if (_dobPossibilities == 0)
            {
                _dobPossibilities = (MaximumAge - MinimumAge) * 365;
                _dobClearThreshold = (int)(_dobPossibilities * 0.95);
                _dobHistory.Clear();
            }

            if (_dobHistory.Count >= _dobClearThreshold)
            {
                _dobHistory.Clear();
            }

            var isUnique = false;
            var dob = DateTime.MinValue;

            while (!isUnique)
            {
                var years = _random.Next(MinimumAge, MaximumAge);
                var days = _random.Next(0, 365);

                var ts = new TimeSpan((years * 365) + days, 0, 0);

                dob = DateTime.Today.Subtract(ts).Date;

                if (_dobHistory.ContainsKey(dob))
                {
                    continue;
                }

                _dobHistory.Add(dob, dob);
                isUnique = true;
            }

            return dob;
        }

        #endregion

        /// <summary>
        /// Generates a single identity base on defined settings.
        /// </summary>
        /// <returns>
        /// Single Random <see cref="IIdentity" />
        /// </returns>
        public IIdentity Generate()
        {
            var name = GenerateName(Genders);
            var ssn = IncludeSSN ? GenerateSSN() : string.Empty;
            var dob = IncludeDOB ? (DateTime?)GenerateDOB() : null;
            var address = IncludeAddress ? GenerateAddress() : null;

            return new Identity
                       {
                           Name = name,
                           SSN = ssn,
                           DOB = dob,
                           Address = address
                       };
        }

        /// <summary>
        /// Generates multiple identities base on defined settings.
        /// </summary>
        /// <param name="number">Number of identities to return.</param>
        /// <returns>
        ///   <see cref="IEnumerable{IIdentity}" />
        /// </returns>
        public IEnumerable<IIdentity> Generate(int number)
        {
            for (var i = 0; i < number; i++)
            {
                yield return Generate();
            }
        }

        /// <summary>
        /// Generates a CSV file.
        /// </summary>
        /// <param name="number">The number identities to generate.</param>
        /// <param name="delimiter">The file record delimiter.</param>
        /// <param name="filename">The output filename.</param>
        public void GenerateToFile(int number, string delimiter, string filename)
        {
            using (var sw = new StreamWriter(filename, true))
            {
                foreach (var identity in Generate(number))
                {
                    sw.Write(string.Format("{1}{0}{2}{0}{2}{0}{3}{0}{4}", delimiter, identity.Name.First, identity.Name.Middle, identity.Name.Last, identity.Name.Gender));

                    if (IncludeSSN)
                    {
                        sw.Write(string.Format("{0}{1}", delimiter, identity.SSN));
                    }

                    if (IncludeDOB)
                    {
                        sw.Write(string.Format("{0}{1}", delimiter, identity.DOB));
                    }

                    if (IncludeAddress)
                    {
                        sw.Write(string.Format("{0}{1}{0}{2}{0}{3}{0}{4}{0}{5}", delimiter, identity.Address.AddressLine, identity.Address.City, identity.Address.State.Name, identity.Address.State.Abbreviation, identity.Address.ZipCode));
                    }

                    sw.WriteLine();
                }
            }
        }

        internal void LoadExternalNameData(INameData nameData)
        {
            NameData = nameData;
        }

        internal void LoadInternalNameData(GenderFilter genderFilter)
        {
            string line;

            var firstNames = new List<IFirstNameData>();
            var lastNames = new List<string>();

            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("EfficientlyLazy.IdentityGenerator.DataFiles.NamesFirst.data"))
            {
                if (stream == null)
                {
                    throw new InvalidOperationException("NamesFirst.data Embedded Resource Not Found");
                }

                using (var cr = new GZipStream(stream, CompressionMode.Decompress))
                {
                    using (var sr = new StreamReader(cr))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            var parts = line.Split('|');

                            switch (parts[1])
                            {
                                case "M": // Male
                                    firstNames.Add(new FirstNameData
                                    {
                                        Gender = Gender.Male,
                                        Name = parts[0]
                                    });
                                    break;
                                case "F": // Female
                                    firstNames.Add(new FirstNameData
                                    {
                                        Gender = Gender.Female,
                                        Name = parts[0]
                                    });
                                    break;
                                case "B": // Male or Female
                                    firstNames.Add(new FirstNameData
                                    {
                                        Gender = Gender.Male,
                                        Name = parts[0]
                                    });
                                    firstNames.Add(new FirstNameData
                                    {
                                        Gender = Gender.Female,
                                        Name = parts[0]
                                    });
                                    break;
                                default:
                                    throw new InvalidOperationException(string.Format("Invalid Line: '{0}'", line));
                            }
                        }
                    }
                }
            }

            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("EfficientlyLazy.IdentityGenerator.DataFiles.NamesLast.data"))
            {
                if (stream == null)
                {
                    throw new InvalidOperationException("NamesLast.txt Embedded Resource Not Found");
                }

                using (var cr = new GZipStream(stream, CompressionMode.Decompress))
                {
                    using (var sr = new StreamReader(cr))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            lastNames.Add(line);
                        }
                    }
                }
            }

            NameData = new NameData
                {
                    FirstNameData = firstNames,
                    GenderFilter = genderFilter,
                    LastNameData = lastNames
                };
        }

        internal void LoadExternalAddressData(IAddressData addressData)
        {
            AddressData = addressData;
        }

        internal void LoadInternalAddressData()
        {
            var cszList = new List<ICityStateZipData>();

            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("EfficientlyLazy.IdentityGenerator.DataFiles.CityStateZips.data"))
            {
                if (stream == null)
                {
                    throw new InvalidOperationException("CityStateZips.data Embedded Resource Not Found");
                }

                using (var cr = new GZipStream(stream, CompressionMode.Decompress))
                {
                    using (var sr = new StreamReader(cr))
                    {
                        string line;

                        while ((line = sr.ReadLine()) != null)
                        {
                            var parts = line.Split('|');

                            var stateName = parts[0];
                            var city = parts[2];
                            var stateAbbreviation = parts[1];
                            var zip = parts[3];

                            cszList.Add(new CityStateZipData
                            {
                                City = city,
                                StateAbbreviation = stateAbbreviation,
                                StateName = stateName,
                                ZipCode = zip
                            });
                        }
                    }
                }
            }

            var streetTypes = new List<string>
                    {
                        "St",
                        "Ave"
                    };

            var directions = new List<string>
                    {
                        "N",
                        "NW",
                        "W",
                        "SW",
                        "S",
                        "SE",
                        "E",
                        "NE"
                    };

            AddressData = new AddressData
                {
                    CityStateZips = cszList,
                    Directions = directions,
                    StreetTypes = streetTypes
                };
        }

        internal void LoadExternalSSNAreaCodeData(IEnumerable<ISSNAreaCodeData> ssnAreaCodeData)
        {
            SSNAreaCodeData = ssnAreaCodeData;
        }

        internal void LoadInternalSSNAreaCodeData()
        {
            var ssnAreaCodes = new List<ISSNAreaCodeData>();

            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("EfficientlyLazy.IdentityGenerator.DataFiles.SSNAreaCodes.data"))
            {
                if (stream == null)
                {
                    throw new InvalidOperationException("SSNAreaCodes.data Embedded Resource Not Found");
                }

                using (var cr = new GZipStream(stream, CompressionMode.Decompress))
                {
                    using (var sr = new StreamReader(cr))
                    {
                        string line;

                        while ((line = sr.ReadLine()) != null)
                        {
                            var parts = line.Split('|');

                            ssnAreaCodes.Add(new SSNAreaCodeData
                            {
                                StateAbbreviation = parts[0],
                                Minimum = int.Parse(parts[1]),
                                Maximum = int.Parse(parts[2])
                            });
                        }
                    }
                }
            }

            SSNAreaCodeData = ssnAreaCodes;
        }
    }
}
