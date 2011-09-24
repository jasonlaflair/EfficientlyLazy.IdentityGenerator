using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using EfficientlyLazy.IdentityGenerator.Entity;

namespace EfficientlyLazy.IdentityGenerator
{
    public class Generator : IGenerator
    {
        #region Pre-Processing
        
        private static readonly List<IFirstNameData> FirstNames = new List<IFirstNameData>();
        private static readonly List<string> LastNames = new List<string>();
        private static readonly List<ICityStateZipData> CityStateZips = new List<ICityStateZipData>();
        private static readonly List<ISSNAreaCodeData> SSNAreaCodes = new List<ISSNAreaCodeData>();

        private static readonly List<string> StreetTypes = new List<string>
                                                               {
                                                                   "St",
                                                                   "Ave"
                                                               };

        private static readonly List<string> Directions = new List<string>
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

        internal static readonly Random Random;

        static Generator()
        {
            string line;

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
                                    FirstNames.Add(new FirstNameData
                                                       {
                                                           Gender = Gender.Male,
                                                           Name = parts[0]
                                                       });
                                    break;
                                case "F": // Female
                                    FirstNames.Add(new FirstNameData
                                                       {
                                                           Gender = Gender.Female,
                                                           Name = parts[0]
                                                       });
                                    break;
                                case "B": // Male or Female
                                    FirstNames.Add(new FirstNameData
                                                       {
                                                           Gender = Gender.Male,
                                                           Name = parts[0]
                                                       });
                                    FirstNames.Add(new FirstNameData
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
                            LastNames.Add(line);
                        }
                    }
                }
            }

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
                        while ((line = sr.ReadLine()) != null)
                        {
                            var parts = line.Split('|');

                            var stateName = parts[0];
                            var city = parts[2];
                            var stateAbbreviation = parts[1];
                            var zip = parts[3];

                            CityStateZips.Add(new CityStateZipData
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
                        while ((line = sr.ReadLine()) != null)
                        {
                            var parts = line.Split('|');

                            SSNAreaCodes.Add(new SSNAreaCodeData
                                                 {
                                                     StateAbbreviation = parts[0],
                                                     Minimum = int.Parse(parts[1]),
                                                     Maximum = int.Parse(parts[2])
                                                 });
                        }
                    }
                }
            }

            FirstNames = FirstNames.OrderBy(x => x.Name).ToList();
            LastNames = LastNames.OrderBy(x => x).ToList();
            CityStateZips = CityStateZips.OrderBy(x => x.StateAbbreviation).ThenBy(x => x.City).ToList();
            
            Random = new Random(DateTime.Now.Millisecond);
        }
        
        #endregion

        #region DEFAULTS
        private const bool DEFAULT_INCLUDE_SSN = false;
        private const bool DEFAULT_INCLUDE_DOB = false;
        private const bool DEFAULT_INCLUDE_ADDRESS = false;
        private const GenderFilter DEFAULT_GENDER_FILTER = GenderFilter.Both;

        private const int DEFAULT_MINIMUM_AGE = 1;
        private const int DEFAULT_MAXIMUM_AGE = 100;
        #endregion

        public static IGeneratorOptions Configure()
        {
            return new GeneratorOptions();
        }

        private class GeneratorOptions : IGeneratorOptions
        {
            private bool _includeSSN = DEFAULT_INCLUDE_SSN;
            private bool _includeDOB = DEFAULT_INCLUDE_DOB;
            private bool _includeAddress = DEFAULT_INCLUDE_ADDRESS;
            private GenderFilter _genderFilter = DEFAULT_GENDER_FILTER;

            private int _minimumAge = DEFAULT_MINIMUM_AGE;
            private int _maximumAge = DEFAULT_MAXIMUM_AGE;

            public IGeneratorOptions IncludeSSN()
            {
                _includeSSN = true;
                return this;
            }

            public IGeneratorOptions ExcludeSSN()
            {
                _includeSSN = false;
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
                return this;
            }

            public IGeneratorOptions ExcludeAddress()
            {
                _includeAddress = false;
                return this;
            }

            public IGeneratorOptions SetGenderFilter(GenderFilter filter)
            {
                _genderFilter = filter;
                return this;
            }

            public IGenerator Build()
            {
                return new Generator
                           {
                               IncludeAddress = _includeAddress,
                               IncludeDOB = _includeDOB,
                               Genders = _genderFilter,
                               IncludeSSN = _includeSSN,
                               MaximumAge = _maximumAge,
                               MinimumAge = _minimumAge
                           };
            }
        }

        private Generator()
        {
            IncludeSSN = DEFAULT_INCLUDE_SSN;
            IncludeDOB = DEFAULT_INCLUDE_DOB;
            IncludeAddress = DEFAULT_INCLUDE_ADDRESS;
            Genders = DEFAULT_GENDER_FILTER;

            MinimumAge = DEFAULT_MINIMUM_AGE;
            MaximumAge = DEFAULT_MAXIMUM_AGE;
        }

        public bool IncludeSSN { get; private set; }
        public bool IncludeDOB { get; private set; }
        public bool IncludeAddress { get; private set; }
        public GenderFilter Genders { get; private set; }

        public int MinimumAge { get; private set; }
        public int MaximumAge { get; private set; }

        public static IName GenerateName(GenderFilter filter)
        {
            IFirstNameData first;

            switch (filter)
            {
                case GenderFilter.Female:
                    first = FirstNames.GetRandom(x => x.Gender == Gender.Female);
                    break;
                case GenderFilter.Male:
                    first = FirstNames.GetRandom(x => x.Gender == Gender.Male);
                    break;
                case GenderFilter.Both:
                    first = FirstNames.GetRandom();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("filter");
            }

            var middle = FirstNames.GetRandom(x => x.Gender == first.Gender);
            var last = LastNames.GetRandom();

            return new Name
                       {
                           First = first.Name,
                           Middle = middle.Name,
                           Last = last,
                           Gender = first.Gender
                       };
        }

        public static string GenerateSSN()
        {
            return GenerateSSN(string.Empty);
        }

        public static string GenerateSSN(string stateAbbreviation)
        {
            var ssn = string.Empty;
            
            var areaCode = SSNAreaCodes.FirstOrDefault(x => x.StateAbbreviation == stateAbbreviation);

            if (areaCode != null)
            {
                var digits = Random.Next(areaCode.Minimum, areaCode.Maximum + 1);
                ssn = digits.ToString().PadLeft(3, '0');
            }

            do
            {
                var digit = Random.Next(0, 10);
                ssn += digit.ToString();
            }
            while (ssn.Length < 9);

            return ssn;
        }

        public static IAddress GenerateAddress()
        {
            var cityStateZip = CityStateZips.GetRandom();

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

            var streetType = StreetTypes.GetRandom();
            var direction = Directions.GetRandom();

            var streetNumber = Random.Next(1000, 10000);
            var street = Random.Next(1, 100);

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

        public static DateTime GenerateDOB(int youngestAge, int oldestAge)
        {
            var age = Random.Next(youngestAge, oldestAge);
            var ageDays = Random.Next(0, 365);

            return DateTime.Now.AddYears(age * -1).AddDays(ageDays).Date;
        }

        public IIdentity Generate()
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

        public IEnumerable<IIdentity> Generate(int number)
        {
            for (var i = 0; i < number; i++)
            {
                yield return Generate();
            }
        }

        public void Generate(int number, string delimiter, string filename)
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
    }
}
