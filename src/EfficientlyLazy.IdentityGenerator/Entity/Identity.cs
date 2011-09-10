using System;

namespace EfficientlyLazy.IdentityGenerator.Entity
{
    public class Identity
    {
        public string First { get; internal set; }
        public string Middle { get; internal set; }
        public string Last { get; internal set; }
        public Gender Gender { get; internal set; }
        public Address Address { get; internal set; }
        public string SSN { get; internal set; }
        public DateTime? DOB { get; internal set; }
    }
}