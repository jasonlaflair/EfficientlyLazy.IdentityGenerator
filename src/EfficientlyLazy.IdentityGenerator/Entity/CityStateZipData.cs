using System;

namespace EfficientlyLazy.IdentityGenerator.Entity
{
    internal class CityStateZipData : ICityStateZipData
    {
        public string City { get; set; }
        public string StateName { get; set; }
        public string StateAbbreviation { get; set; }
        public string ZipCode { get; set; }

        public bool Equals(ICityStateZipData other)
        {
            return City.Equals(other.City, StringComparison.CurrentCultureIgnoreCase)
                   && StateName.Equals(other.StateName, StringComparison.CurrentCultureIgnoreCase)
                   && StateAbbreviation.Equals(other.StateAbbreviation, StringComparison.CurrentCultureIgnoreCase)
                   && ZipCode.Equals(other.ZipCode, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}