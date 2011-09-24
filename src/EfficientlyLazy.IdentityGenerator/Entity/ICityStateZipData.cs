namespace EfficientlyLazy.IdentityGenerator.Entity
{
    public interface ICityStateZipData
    {
        string City { get; }
        string StateName { get; }
        string StateAbbreviation { get; }
        string ZipCode { get; }
    }
}