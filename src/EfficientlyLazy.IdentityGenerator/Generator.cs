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
        
        private class FirstName
        {
            public string Name { get; set; }
            public Gender Gender { get; set; }
        }

        private class SSNAreaCode
        {
            public int Minimum { get; set; }
            public int Maximum { get; set; }
            public string StateAbbreviation { get; set; }

            public SSNAreaCode(int min, int max, string stateAbbreviation)
            {
                Minimum = min;
                Maximum = max;
                StateAbbreviation = stateAbbreviation;
            }
        }

        private class CityStateZip
        {
            public string City { get; set; }
            public string StateName { get; set; }
            public string StateAbbreviation { get; set; }
            public string ZipCode { get; set; }
        }

        private static readonly List<FirstName> FirstNames = new List<FirstName>();
        private static readonly List<string> LastNames = new List<string>();
        private static readonly List<CityStateZip> CityStateZips = new List<CityStateZip>();

        private static readonly List<SSNAreaCode> SSNAreaCodes = new List<SSNAreaCode>
                                                                     {
                                                                         new SSNAreaCode(416, 424, "AL"),
                                                                         new SSNAreaCode(574, 574, "AK"),
                                                                         new SSNAreaCode(526, 527, "AZ"),
                                                                         new SSNAreaCode(600, 601, "AZ"),
                                                                         new SSNAreaCode(764, 765, "AZ"),
                                                                         new SSNAreaCode(429, 432, "AR"),
                                                                         new SSNAreaCode(676, 679, "AR"),
                                                                         new SSNAreaCode(545, 573, "CA"),
                                                                         new SSNAreaCode(602, 626, "CA"),
                                                                         new SSNAreaCode(521, 524, "CO"),
                                                                         new SSNAreaCode(650, 653, "CO"),
                                                                         new SSNAreaCode(40, 49, "CT"),
                                                                         new SSNAreaCode(221, 222, "DE"),
                                                                         new SSNAreaCode(261, 267, "FL"),
                                                                         new SSNAreaCode(589, 595, "FL"),
                                                                         new SSNAreaCode(766, 772, "FL"),
                                                                         new SSNAreaCode(252, 260, "GA"),
                                                                         new SSNAreaCode(667, 675, "GA"),
                                                                         new SSNAreaCode(575, 576, "HI"),
                                                                         new SSNAreaCode(750, 751, "HI"),
                                                                         new SSNAreaCode(518, 519, "ID"),
                                                                         new SSNAreaCode(318, 361, "IL"),
                                                                         new SSNAreaCode(303, 317, "IN"),
                                                                         new SSNAreaCode(478, 485, "IA"),
                                                                         new SSNAreaCode(509, 515, "KS"),
                                                                         new SSNAreaCode(400, 407, "KY"),
                                                                         new SSNAreaCode(433, 439, "LA"),
                                                                         new SSNAreaCode(659, 665, "LA"),
                                                                         new SSNAreaCode(4, 7, "ME"),
                                                                         new SSNAreaCode(212, 220, "MD"),
                                                                         new SSNAreaCode(10, 34, "MA"),
                                                                         new SSNAreaCode(362, 386, "MI"),
                                                                         new SSNAreaCode(468, 477, "MN"),
                                                                         new SSNAreaCode(425, 428, "MS"),
                                                                         new SSNAreaCode(587, 588, "MS"),
                                                                         new SSNAreaCode(752, 755, "MS"),
                                                                         new SSNAreaCode(486, 500, "MO"),
                                                                         new SSNAreaCode(516, 517, "MT"),
                                                                         new SSNAreaCode(505, 508, "NE"),
                                                                         new SSNAreaCode(530, 680, "NV"),
                                                                         new SSNAreaCode(1, 3, "NH"),
                                                                         new SSNAreaCode(135, 158, "NJ"),
                                                                         new SSNAreaCode(525, 585, "NM"),
                                                                         new SSNAreaCode(648, 649, "NM"),
                                                                         new SSNAreaCode(50, 134, "NY"),
                                                                         new SSNAreaCode(232, 232, "NC"),
                                                                         new SSNAreaCode(237, 246, "NC"),
                                                                         new SSNAreaCode(681, 690, "NC"),
                                                                         new SSNAreaCode(501, 502, "ND"),
                                                                         new SSNAreaCode(268, 302, "OH"),
                                                                         new SSNAreaCode(440, 448, "OK"),
                                                                         new SSNAreaCode(540, 544, "OR"),
                                                                         new SSNAreaCode(159, 211, "PA"),
                                                                         new SSNAreaCode(35, 39, "RI"),
                                                                         new SSNAreaCode(247, 251, "SC"),
                                                                         new SSNAreaCode(654, 658, "SC"),
                                                                         new SSNAreaCode(503, 504, "SD"),
                                                                         new SSNAreaCode(408, 415, "TN"),
                                                                         new SSNAreaCode(756, 763, "TN"),
                                                                         new SSNAreaCode(449, 467, "TX"),
                                                                         new SSNAreaCode(627, 645, "TX"),
                                                                         new SSNAreaCode(528, 529, "UT"),
                                                                         new SSNAreaCode(646, 647, "UT"),
                                                                         new SSNAreaCode(8, 9, "VT"),
                                                                         new SSNAreaCode(223, 231, "VA"),
                                                                         new SSNAreaCode(691, 699, "VA"),
                                                                         new SSNAreaCode(531, 539, "WA"),
                                                                         new SSNAreaCode(232, 236, "WV"),
                                                                         new SSNAreaCode(387, 399, "WI"),
                                                                         new SSNAreaCode(520, 520, "WY"),
                                                                     };

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
                    throw new InvalidOperationException("NamesFirst.txt Embedded Resource Not Found");
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
                                    FirstNames.Add(new FirstName
                                                       {
                                                           Gender = Gender.Male,
                                                           Name = parts[0]
                                                       });
                                    break;
                                case "F": // Female
                                    FirstNames.Add(new FirstName
                                                       {
                                                           Gender = Gender.Female,
                                                           Name = parts[0]
                                                       });
                                    break;
                                case "B": // Male or Female
                                    FirstNames.Add(new FirstName
                                                       {
                                                           Gender = Gender.Male,
                                                           Name = parts[0]
                                                       });
                                    FirstNames.Add(new FirstName
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
                    throw new InvalidOperationException("CityStateZips.txt Embedded Resource Not Found");
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

                            CityStateZips.Add(new CityStateZip
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

            FirstNames = FirstNames.OrderBy(x => x.Name).ToList();
            LastNames = LastNames.OrderBy(x => x).ToList();
            CityStateZips = CityStateZips.OrderBy(x => x.StateAbbreviation).ThenBy(x => x.City).ToList();

            Random = new Random(DateTime.Now.Millisecond);
        }
        
        #endregion

        public static IGeneratorOptions SetOptions()
        {
            return new GeneratorOptions();
        }

        private class GeneratorOptions : IGeneratorOptions
        {
            private bool _includeSSN = DEFAULT_INCLUDE_SSN;
            private bool _includeDOB = DEFAULT_INCLUDE_DOB;
            private bool _includeAddress = DEFAULT_INCLUDE_ADDRESS;
            private bool _includeMale = DEFAULT_INCLUDE_MALE;
            private bool _includeFemale = DEFAULT_INCLUDE_FEMALE;

            private int _minimumAge = DEFAULT_MINIMUM_AGE;
            private int _maximumAge = DEFAULT_MAXIMUM_AGE;

            public IGeneratorOptions IncludeSSN()
            {
                return IncludeSSN(true);
            }

            public IGeneratorOptions IncludeSSN(bool include)
            {
                _includeSSN = include;
                return this;
            }

            public IGeneratorOptions IncludeDOB()
            {
                return IncludeDOB(true);
            }

            public IGeneratorOptions IncludeDOB(bool include)
            {
                _includeDOB = include;
                return this;
            }

            public IGeneratorOptions IncludeAddress()
            {
                return IncludeAddress(true);
            }

            public IGeneratorOptions IncludeAddress(bool include)
            {
                _includeAddress = include;
                return this;
            }

            public IGeneratorOptions IncludeGenderMale()
            {
                return IncludeGenderMale(true);
            }

            public IGeneratorOptions IncludeGenderMale(bool include)
            {
                _includeMale = include;
                return this;
            }

            public IGeneratorOptions IncludeGenderFemale()
            {
                return IncludeGenderFemale(true);
            }

            public IGeneratorOptions IncludeGenderFemale(bool include)
            {
                _includeFemale = include;
                return this;
            }

            public IGeneratorOptions IncludeGenderBoth()
            {
                _includeMale = true;
                _includeFemale = true;
                return this;
            }

            public IGeneratorOptions SetAgeRange(int minimum, int maximum)
            {
                _minimumAge = minimum;
                _maximumAge = maximum;
                return this;
            }

            public IGenerator CreateGenerator()
            {
                return new Generator
                           {
                               IncludeAddress = _includeAddress,
                               IncludeDOB = _includeDOB,
                               IncludeFemale = _includeFemale,
                               IncludeMale = _includeMale,
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
            IncludeMale = DEFAULT_INCLUDE_MALE;
            IncludeFemale = DEFAULT_INCLUDE_FEMALE;

            MinimumAge = DEFAULT_MINIMUM_AGE;
            MaximumAge = DEFAULT_MAXIMUM_AGE;
        }

        #region DEFAULTS
        private const bool DEFAULT_INCLUDE_SSN = false;
        private const bool DEFAULT_INCLUDE_DOB = false;
        private const bool DEFAULT_INCLUDE_ADDRESS = false;
        private const bool DEFAULT_INCLUDE_MALE = true;
        private const bool DEFAULT_INCLUDE_FEMALE = true;

        private const int DEFAULT_MINIMUM_AGE = 1;
        private const int DEFAULT_MAXIMUM_AGE = 100;
        #endregion

        public bool IncludeSSN { get; private set; }
        public bool IncludeDOB { get; private set; }
        public bool IncludeAddress { get; private set; }
        public bool IncludeMale { get; private set; }
        public bool IncludeFemale { get; private set; }

        public int MinimumAge { get; private set; }
        public int MaximumAge { get; private set; }

        public static Name GenerateName()
        {
            return GenerateName(true, true);
        }

        public static Name GenerateName(bool includeFemale, bool includeMale)
        {
            FirstName first;

            if (includeFemale && includeMale)
            {
                first = FirstNames.GetRandom();
            }
            else if (includeFemale)
            {
                first = FirstNames.GetRandom(x => x.Gender == Gender.Female);
            }
            else
            {
                first = FirstNames.GetRandom(x => x.Gender == Gender.Male);
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

        public static string GenerateSSN()
        {
            return GenerateSSN(string.Empty);
        }

        public static Address GenerateAddress()
        {
            var cityStateZip = CityStateZips.GetRandom();

            var address = new Address
                              {
                                  City = cityStateZip.City,
                                  StateAbbreviation = cityStateZip.StateAbbreviation,
                                  StateName = cityStateZip.StateName,
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

        public Identity GenerateIdentity()
        {
            var name = GenerateName(IncludeFemale, IncludeMale);
            var ssn = IncludeSSN ? GenerateSSN() : string.Empty;
            var dob = IncludeDOB ? (DateTime?)GenerateDOB(MinimumAge, MaximumAge) : null;
            var address = IncludeAddress ? GenerateAddress() : null;

            return new Identity
                       {
                           First = name.First,
                           Middle = name.Middle,
                           Last = name.Last,
                           Gender = name.Gender,
                           SSN = ssn,
                           DOB = dob,
                           Address = address
                       };
        }

        public IEnumerable<Identity> GenerateIdentities(int number)
        {
            for (var i = 0; i < number; i++)
            {
                yield return GenerateIdentity();
            }
        }

        public void GenerateIdentities(int number, string delimiter, string filename)
        {
            using (var sw = new StreamWriter(filename, true))
            {
                foreach (var identity in GenerateIdentities(number))
                {
                    sw.Write(string.Format("{1}{0}{2}{0}{2}{0}{3}{0}{4}", delimiter, identity.First, identity.Middle, identity.Last, identity.Gender));

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
                        sw.Write(string.Format("{0}{1}{0}{2}{0}{3}{0}{4}{0}{5}", delimiter, identity.Address.AddressLine, identity.Address.City, identity.Address.StateName, identity.Address.StateAbbreviation, identity.Address.ZipCode));
                    }

                    sw.WriteLine();
                }
            }
        }
    }
}
