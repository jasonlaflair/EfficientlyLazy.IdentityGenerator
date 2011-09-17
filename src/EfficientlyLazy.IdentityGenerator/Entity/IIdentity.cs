using System;

namespace EfficientlyLazy.IdentityGenerator.Entity
{
    public interface IIdentity
    {
        IName Name { get; }
        IAddress Address { get; }
        string SSN { get; }
        DateTime? DOB { get; }
    }
}