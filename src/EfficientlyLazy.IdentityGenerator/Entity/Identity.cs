using System;

namespace EfficientlyLazy.IdentityGenerator.Entity
{
    public class Identity
    {
        public string First { get; internal set; }
        public string Middle { get; internal set; }
        public string Last { get; internal set; }
        public string Gender { get; internal set; }
        public string Address { get; internal set; }
        public string City { get; internal set; }
        public string StateAbbreviation { get; internal set; }
        public string StateName { get; internal set; }
        public string ZipCode { get; internal set; }
        public string SSN { get; internal set; }
        public DateTime DOB { get; internal set; }
    }
}