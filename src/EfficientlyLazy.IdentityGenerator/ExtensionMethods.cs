using System;
using System.Collections.Generic;
using System.Linq;

namespace EfficientlyLazy.IdentityGenerator
{
    internal static class ExtensionMethods
    {
        public static T GetRandom<T>(this IEnumerable<T> data, Func<T, bool> where)
        {
            var list = data.Where(where).ToList();

            return list[Generator.Random.Next(0, list.Count)];
        }

        public static T GetRandom<T>(this IList<T> list)
        {
            return list[Generator.Random.Next(0, list.Count)];
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