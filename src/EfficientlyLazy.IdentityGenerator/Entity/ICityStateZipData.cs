using System;

namespace EfficientlyLazy.IdentityGenerator.Entity
{
    /// <summary></summary>
    public interface ICityStateZipData : IEquatable<ICityStateZipData>
    {
        /// <summary></summary>
        string City { get; }

        /// <summary></summary>
        string StateName { get; }

        /// <summary></summary>
        string StateAbbreviation { get; }

        /// <summary></summary>
        string ZipCode { get; }
    }
}