using EfficientlyLazy.IdentityGenerator.Entity;

namespace EfficientlyLazy.IdentityGenerator
{
    public interface IGeneratorOptions
    {
        IGeneratorOptions IncludeSSN();
        IGeneratorOptions ExcludeSSN();
        IGeneratorOptions IncludeDOB(int minimum, int maximum);
        IGeneratorOptions ExcludeDOB();
        IGeneratorOptions IncludeAddress();
        IGeneratorOptions ExcludeAddress();
        IGeneratorOptions SetGenderFilter(GenderFilter filter);

        IGenerator Build();
    }
}