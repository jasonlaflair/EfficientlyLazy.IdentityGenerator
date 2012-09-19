using System;

namespace EfficientlyLazy.IdentityGenerator.Entity
{
    /// <summary></summary>
    public interface IFirstNameData : IEquatable<IFirstNameData>
    {
        /// <summary></summary>
        string Name { get; }

        /// <summary></summary>
        Gender Gender { get; }
    }
}