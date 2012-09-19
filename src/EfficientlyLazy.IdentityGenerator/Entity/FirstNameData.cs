using System;

namespace EfficientlyLazy.IdentityGenerator.Entity
{
    internal class FirstNameData : IFirstNameData
    {
        public string Name { get; set; }
        public Gender Gender { get; set; }

        public bool Equals(IFirstNameData other)
        {
            return Name.Equals(other.Name, StringComparison.CurrentCultureIgnoreCase)
                   && Gender.Equals(other.Gender);
        }
    }
}
