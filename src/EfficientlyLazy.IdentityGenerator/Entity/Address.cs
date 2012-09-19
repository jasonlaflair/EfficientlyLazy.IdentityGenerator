using System;

namespace EfficientlyLazy.IdentityGenerator.Entity
{
    internal class Address : IAddress
    {
        public string AddressLine { get; set; }
        public string City { get; set; }
        public IState State { get; set; }
        public string ZipCode { get; set; }

        public bool Equals(IAddress other)
        {
            return AddressLine.Equals(other.AddressLine, StringComparison.CurrentCultureIgnoreCase)
                   && City.Equals(other.City, StringComparison.CurrentCultureIgnoreCase)
                   && State.Equals(other.State)
                   && ZipCode.Equals(other.ZipCode, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}