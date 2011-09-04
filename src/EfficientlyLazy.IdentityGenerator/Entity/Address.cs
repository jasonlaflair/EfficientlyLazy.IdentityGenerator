namespace EfficientlyLazy.IdentityGenerator.Entity
{
    public class Address
    {
        public string AddressLine { get; internal set; }
        public string City { get; internal set; }
        public string StateAbbreviation { get; internal set; }
        public string StateName { get; internal set; }
        public string ZipCode { get; internal set; }
    }
}