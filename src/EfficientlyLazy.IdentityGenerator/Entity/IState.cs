using System;

namespace EfficientlyLazy.IdentityGenerator.Entity
{
    /// <summary></summary>
    public interface IState : IEquatable<IState>
    {
        /// <summary></summary>
        string Name { get; }
        
        /// <summary></summary>
        string Abbreviation { get; }
    }
}