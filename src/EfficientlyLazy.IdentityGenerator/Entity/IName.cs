using System;

namespace EfficientlyLazy.IdentityGenerator.Entity
{
    /// <summary></summary>
    public interface IName : IEquatable<IName>
    {
        /// <summary></summary>
        string First { get; }

        /// <summary></summary>
        string Middle { get; }
        
        /// <summary></summary>
        string Last { get; }
        
        /// <summary></summary>
        Gender Gender { get; }
    }
}