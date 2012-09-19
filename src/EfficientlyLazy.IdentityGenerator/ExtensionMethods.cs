using System;
using System.Collections.Generic;
using System.Linq;

namespace EfficientlyLazy.IdentityGenerator
{
    internal static class ExtensionMethods
    {
        private static readonly Random _random;

        static ExtensionMethods()
        {
            _random = RandomCreator.GenerateCryptographicSeededRandom();
        }

        public static T GetRandom<T>(this IEnumerable<T> data, Func<T, bool> where)
        {
            var list = data.Where(where);

            var index = _random.Next(0, list.Count());

            return list.Skip(index).Take(1).Single();
        }

        public static T GetRandom<T>(this IEnumerable<T> data)
        {
            var index = _random.Next(0, data.Count());

            return data.Skip(index).Take(1).Single();
        }

        public static string ToProperCase(this string value)
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