using System;

namespace EfficientlyLazy.IdentityGenerator.Entity
{
    internal class Name : IName
    {
        public string First { get; set; }
        public string Middle { get; set; }
        public string Last { get; set; }
        public Gender Gender { get; set; }

        public bool Equals(IName other)
        {
            return First.Equals(other.First, StringComparison.CurrentCultureIgnoreCase)
                   && Middle.Equals(other.Middle, StringComparison.CurrentCultureIgnoreCase)
                   && Last.Equals(other.Last, StringComparison.CurrentCultureIgnoreCase)
                   && Gender.Equals(other.Gender);
        }
    }
}