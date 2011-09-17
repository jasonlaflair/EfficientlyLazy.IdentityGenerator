using System;

namespace EfficientlyLazy.IdentityGenerator.Entity
{
    internal class Identity : IIdentity
    {
        public IName Name { get; set; }
        public IAddress Address { get; set; }
        public string SSN { get; set; }
        public DateTime? DOB { get; set; }
    }
}