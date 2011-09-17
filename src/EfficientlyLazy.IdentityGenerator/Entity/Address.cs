namespace EfficientlyLazy.IdentityGenerator.Entity
{
    internal class Address : IAddress
    {
        public string AddressLine { get; set; }
        public string City { get; set; }
        public IState State { get; set; }
        public string ZipCode { get; set; }
    }
}