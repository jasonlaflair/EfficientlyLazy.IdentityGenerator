using System;

namespace EfficientlyLazy.IdentityGenerator.Entity
{
    /// <summary></summary>
    public interface ISSNAreaCodeData : IEquatable<ISSNAreaCodeData>
    {
        /// <summary></summary>
        int Minimum { get; }

        /// <summary></summary>
        int Maximum { get; }

        /// <summary></summary>
        string StateAbbreviation { get; }
    }
}