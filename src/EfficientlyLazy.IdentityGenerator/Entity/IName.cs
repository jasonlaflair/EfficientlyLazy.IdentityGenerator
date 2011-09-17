namespace EfficientlyLazy.IdentityGenerator.Entity
{
    public interface IName
    {
        string First { get; }
        string Middle { get; }
        string Last { get; }
        Gender Gender { get; }
    }
}