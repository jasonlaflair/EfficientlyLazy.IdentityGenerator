using System;

namespace EfficientlyLazy.IdentityGenerator.Entity
{
    /// <summary></summary>
    public interface IAddress : IEquatable<IAddress>
    {
        /// <summary></summary>
        string AddressLine { get; }

        /// <summary></summary>
        string City { get; }

        /// <summary></summary>
        IState State { get; }

        /// <summary></summary>
        string ZipCode { get; }
    }
}