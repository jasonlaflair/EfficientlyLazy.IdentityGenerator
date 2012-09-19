using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using EfficientlyLazy.IdentityGenerator.Entity;

namespace EfficientlyLazy.IdentityGenerator
{
    /// <summary></summary>
    public class Generator : IGenerator
    {
        internal INameData NameData;
        internal bool InternalNameData;
        internal IAddressData AddressData;
        internal bool InternalAddressData;
        internal IEnumerable<ISSNAreaCodeData> SSNAreaCodeData;
        internal bool InternalSSNAreaCodeData;

        private readonly Random _random;

        #region DEFAULTS
        private const GenderFilter DEFAULT_GENDER_FILTER = GenderFilter.Both;
        private const int DEFAULT_MINIMUM_AGE = 1;
        private const int DEFAULT_MAXIMUM_AGE = 100;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IGeneratorOptions Configure()
        {
            return new GeneratorOptions();
        }

        internal class GeneratorOptions : IGeneratorOptions
        {
            private bool _includeName;
            private bool _includeSSN;
            private bool _includeDOB;
            private bool _includeAddress;
            private GenderFilter _genderFilter = DEFAULT_GENDER_FILTER;

            private int _minimumAge = DEFAULT_MINIMUM_AGE;
            private int _maximumAge = DEFAULT_MAXIMUM_AGE;

            private INameData _nameData;
            private IAddressData _addressData;
            private IEnumerable<ISSNAreaCodeData> _ssnAreaCodeData;

            public IGeneratorOptions IncludeName()
            {
                _includeName = true;
                _genderFilter = GenderFilter.Both;
                _nameData = null;

                return this;
            }

            public IGeneratorOptions IncludeName(GenderFilter filter)
            {
                _includeName = true;
                _genderFilter = filter;
                _nameData = null;

                return this;
            }

            public IGeneratorOptions IncludeName(INameData nameData)
            {
                _includeName = true;
                _genderFilter = nameData.GenderFilter;
                _nameData = nameData;

                return this;
            }

            public IGeneratorOptions ExcludeName()
            {
                _includeName = false;
                _genderFilter = GenderFilter.Both;
                _nameData = null;

                return this;
            }

            public IGeneratorOptions IncludeSSN()
            {
                _includeSSN = true;
                _ssnAreaCodeData = null;

                return this;
            }

            public IGeneratorOptions IncludeSSN(IEnumerable<ISSNAreaCodeData> ssnAreaCodeData)
            {
                _includeSSN = true;
                _ssnAreaCodeData = ssnAreaCodeData;

                return this;
            }

            public IGeneratorOptions ExcludeSSN()
            {
                _includeSSN = false;
                _ssnAreaCodeData = null;

                return this;
            }

            public IGeneratorOptions IncludeDOB()
            {
                _includeDOB = true;
                _minimumAge = DEFAULT_MINIMUM_AGE;
                _maximumAge = DEFAULT_MAXIMUM_AGE;

                return this;
            }

            public IGeneratorOptions IncludeDOB(int minimum, int maximum)
            {
                _includeDOB = true;
                _minimumAge = minimum;
                _maximumAge = maximum;

                return this;
            }

            public IGeneratorOptions ExcludeDOB()
            {
                _includeDOB = false;
                _minimumAge = DEFAULT_MINIMUM_AGE;
                _maximumAge = DEFAULT_MAXIMUM_AGE;

                return this;
            }

            public IGeneratorOptions IncludeAddress()
            {
                _includeAddress = true;
                _addressData = null;

                return this;
            }

            public IGeneratorOptions IncludeAddress(IAddressData addressData)
            {
                _includeAddress = true;
                _addressData = addressData;

                return this;
            }

            public IGeneratorOptions ExcludeAddress()
            {
                _includeAddress = false;
                _addressData = null;
                return this;
            }

            public IGenerator Build()
            {
                var generator = new Generator
                    {
                        IncludeName = _includeName,
                        IncludeAddress = _includeAddress,
                        IncludeDOB = _includeDOB,
                        Genders = _genderFilter,
                        IncludeSSN = _includeSSN,
                        MaximumAge = _maximumAge,
                        MinimumAge = _minimumAge
                    };

                if (_includeName && _nameData == null)
                {
                    generator.LoadInternalNameData(_genderFilter);
                }
                else if (_includeName)
                {
                    generator.LoadInternalNameData(_nameData);
                }

                if (_includeAddress && _addressData == null)
                {
                    generator.LoadInternalAddressData();
                }
                else if (_includeAddress)
                {
                    generator.LoadInternalAddressData(_addressData);
                }

                if (_includeSSN && _ssnAreaCodeData == null)
                {
                    generator.LoadInternalSSNAreaCodeData();
                }
                else if (_includeSSN)
                {
                    generator.LoadInternalSSNAreaCodeData(_ssnAreaCodeData);
                }

                return generator;
            }
        }

        private Generator()
        {
            _random = RandomCreator.GenerateCryptographicSeededRandom();
        }

        public bool IncludeName { get; private set; }
        public bool IncludeSSN { get; private set; }
        public bool IncludeDOB { get; private set; }
        public bool IncludeAddress { get; private set; }
        public GenderFilter Genders { get; private set; }

        public int MinimumAge { get; private set; }
        public int MaximumAge { get; private set; }

        public virtual IName GenerateName(GenderFilter filter)
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

        public virtual string GenerateSSN()
        {
            return GenerateSSN(string.Empty);
        }

        public virtual string GenerateSSN(string stateAbbreviation)
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

            return ssn;
        }

        public virtual IAddress GenerateAddress()
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

        public virtual DateTime GenerateDOB()
        {
            return GenerateDOB(MinimumAge, MaximumAge);
        }

        public virtual DateTime GenerateDOB(int youngestAge, int oldestAge)
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

        public virtual IIdentity Generate()
        {
            var name = GenerateName(Genders);
            var ssn = IncludeSSN ? GenerateSSN() : string.Empty;
            var dob = IncludeDOB ? (DateTime?)GenerateDOB(MinimumAge, MaximumAge) : null;
            var address = IncludeAddress ? GenerateAddress() : null;

            return new Identity
                       {
                           Name = name,
                           SSN = ssn,
                           DOB = dob,
                           Address = address
                       };
        }

        public virtual IEnumerable<IIdentity> Generate(int number)
        {
            for (var i = 0; i < number; i++)
            {
                yield return Generate();
            }
        }

        public virtual void Generate(int number, string delimiter, string filename)
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

        internal virtual void LoadInternalNameData(INameData nameData)
        {
            NameData = nameData;
            InternalNameData = false;
        }

        internal virtual void LoadInternalNameData(GenderFilter genderFilter)
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
            InternalNameData = true;
        }

        internal virtual void LoadInternalAddressData(IAddressData addressData)
        {
            AddressData = addressData;
            InternalAddressData = false;
        }

        internal virtual void LoadInternalAddressData()
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
            InternalAddressData = true;
        }

        internal virtual void LoadInternalSSNAreaCodeData(IEnumerable<ISSNAreaCodeData> ssnAreaCodeData)
        {
            SSNAreaCodeData = ssnAreaCodeData;
            InternalSSNAreaCodeData = false;
        }

        internal virtual void LoadInternalSSNAreaCodeData()
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
            InternalSSNAreaCodeData = true;
        }
    }
}
