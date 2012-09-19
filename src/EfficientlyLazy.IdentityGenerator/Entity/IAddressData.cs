using System.Collections.Generic;

namespace EfficientlyLazy.IdentityGenerator.Entity
{
    /// <summary></summary>
    public interface IAddressData
    {
        /// <summary></summary>
        IEnumerable<ICityStateZipData> CityStateZips { get; }

        /// <summary></summary>
        IEnumerable<string> StreetTypes { get; }

        /// <summary></summary>
        IEnumerable<string> Directions { get; }
    }
}
