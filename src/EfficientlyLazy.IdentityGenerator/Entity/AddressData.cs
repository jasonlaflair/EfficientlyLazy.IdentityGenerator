using System.Collections.Generic;

namespace EfficientlyLazy.IdentityGenerator.Entity
{
    internal class AddressData : IAddressData
    {
        public IEnumerable<ICityStateZipData> CityStateZips { get; set; }
        public IEnumerable<string> StreetTypes { get; set; }
        public IEnumerable<string> Directions { get; set; }
    }
}