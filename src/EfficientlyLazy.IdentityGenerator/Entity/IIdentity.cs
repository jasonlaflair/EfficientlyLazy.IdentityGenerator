using System;

namespace EfficientlyLazy.IdentityGenerator.Entity
{
    /// <summary></summary>
    public interface IIdentity
    {
        /// <summary></summary>
        IName Name { get; }

        /// <summary></summary>
        IAddress Address { get; }

        /// <summary></summary>
        string SSN { get; }

        /// <summary></summary>
        DateTime? DOB { get; }
    }
}