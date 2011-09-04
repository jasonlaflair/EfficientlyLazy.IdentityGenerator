using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using EfficientlyLazy.IdentityGenerator.Entity;

namespace EfficientlyLazy.IdentityGenerator
{
    public static class Generator
    {
        private enum NameGender
        {
            Female,
            Male
        }

        private class FirstName : IComparable<FirstName>
        {
            public string Name { get; set; }
            public NameGender Gender { get; set; }

            public int CompareTo(FirstName other)
            {
                return Name.Equals(other.Name) ? Gender.CompareTo(other.Gender) : Name.CompareTo(other.Name);
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

        public static readonly Random Random;

        static Generator()
        {
            string line;

            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("EfficientlyLazy.IdentityGenerator.DataFiles.NamesFirst.txt"))
            {
                if (stream == null)
                {
                    throw new InvalidOperationException("NamesFirst.txt Embedded Resource Not Found");
                }

                using (var sr = new StreamReader(stream))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        var parts = line.Split('|');

                        switch (parts[1])
                        {
                            case "M": // Male
                                FirstNames.Add(new FirstName
                                                       {
                                                           Gender = NameGender.Male,
                                                           Name = parts[0]
                                                       });
                                break;
                            case "F": // Female
                                FirstNames.Add(new FirstName
                                                       {
                                                           Gender = NameGender.Female,
                                                           Name = parts[0]
                                                       });
                                break;
                            case "B": // Male or Female
                                FirstNames.Add(new FirstName
                                                       {
                                                           Gender = NameGender.Male,
                                                           Name = parts[0]
                                                       });
                                FirstNames.Add(new FirstName
                                                       {
                                                           Gender = NameGender.Female,
                                                           Name = parts[0]
                                                       });
                                break;
                            default:
                                throw new InvalidOperationException(string.Format("Invalid Line: '{0}'", line));
                        }
                    }
                }
            }

            FirstNames.Sort();

            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("EfficientlyLazy.IdentityGenerator.DataFiles.NamesLast.txt"))
            {
                if (stream == null)
                {
                    throw new InvalidOperationException("NamesLast.txt Embedded Resource Not Found");
                }

                using (var sr = new StreamReader(stream))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        LastNames.Add(line);
                    }
                }
            }

            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("EfficientlyLazy.IdentityGenerator.DataFiles.CityStateZips.txt"))
            {
                if (stream == null)
                {
                    throw new InvalidOperationException("CityStateZips.txt Embedded Resource Not Found");
                }

                using (var sr = new StreamReader(stream))
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

            Random = new Random(DateTime.Now.Millisecond);
        }

        public static string GenerateSSN()
        {
            var ssn = string.Empty;
            for (var i = 0; i < 9; i++)
            {
                var digit = Random.Next(0, 10);
                ssn += digit.ToString();
            }

            return ssn;
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

        public static Identity GenerateIdentity()
        {
            var firstName = FirstNames.GetRandom();
            var middleName = FirstNames.GetRandom(x => x.Gender == firstName.Gender);
            var last = LastNames.GetRandom();

            var ssn = GenerateSSN();
            var dob = GenerateDOB(18, 100);
            var address = GenerateAddress();

            return new Identity
                       {
                           First = firstName.Name.ToProperCase(),
                           Middle = middleName.Name.ToProperCase(),
                           Last = last.ToProperCase(),
                           Gender = firstName.Gender.ToString(),
                           SSN = ssn,
                           DOB = dob,
                           Address = address.AddressLine,
                           City = address.City.ToProperCase(),
                           StateAbbreviation = address.StateAbbreviation,
                           StateName = address.StateName.ToProperCase(),
                           ZipCode = address.ZipCode
                       };
        }

        public static IEnumerable<Identity> GenerateIdentities(int number)
        {
            for (var i = 0; i < number; i++)
            {
                
                yield return GenerateIdentity();
            }
        }

        public static void GenerateIdentities(int number, string delimiter, string filename, List<PropertyInfo> identityProperties)
        {
            using (var sw = new StreamWriter(filename, false))
            {
                foreach (var id in GenerateIdentities(number))
                {
                    var values = new List<string>();

                    foreach (var pi in identityProperties)
                    {
                        var value = pi.GetValue(id, null);

                        if (pi.PropertyType == typeof(DateTime))
                        {
                            values.Add(Convert.ToDateTime(value).ToString("MM/dd/yyyy"));
                        }
                        else
                        {
                            values.Add(value.ToString());
                        }
                    }

                    sw.WriteLine(string.Join(delimiter, values.ToArray()));
                }
            }
        }

        private static T GetRandom<T>(this IEnumerable<T> data, Func<T, bool> where)
        {
            var list = data.Where(where).ToList();

            return list[Random.Next(0, list.Count)];
        }

        private static T GetRandom<T>(this IList<T> list)
        {
            return list[Random.Next(0, list.Count)];
        }

        private static string ToProperCase(this string value)
        {
            var proper = string.Empty;

            for (var i = 0; i < value.Length; i++)
            {
                if (i == 0 || value[i - 1] == ' ')
                {
                    proper += value[i].ToString().ToUpper();
                }
                else
                {
                    proper += value[i].ToString().ToLower();
                }
            }

            return proper;
        }
    }
}
