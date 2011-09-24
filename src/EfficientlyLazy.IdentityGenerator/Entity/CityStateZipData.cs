namespace EfficientlyLazy.IdentityGenerator.Entity
{
    internal class CityStateZipData : ICityStateZipData
    {
        public string City { get; set; }
        public string StateName { get; set; }
        public string StateAbbreviation { get; set; }
        public string ZipCode { get; set; }
    }
}