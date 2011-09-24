namespace EfficientlyLazy.IdentityGenerator.Entity
{
    public interface ISSNAreaCodeData
    {
        int Minimum { get; }
        int Maximum { get; }
        string StateAbbreviation { get; }
    }
}