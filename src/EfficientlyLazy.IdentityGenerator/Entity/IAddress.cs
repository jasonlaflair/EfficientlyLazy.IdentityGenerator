namespace EfficientlyLazy.IdentityGenerator.Entity
{
    public interface IAddress
    {
        string AddressLine { get; }
        string City { get; }
        IState State { get; }
        string ZipCode { get; }
    }
}