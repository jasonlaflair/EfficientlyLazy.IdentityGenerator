using EfficientlyLazy.IdentityGenerator.Entity;

namespace EfficientlyLazy.IdentityGenerator
{
    public interface IGeneratorOptions
    {
        IGeneratorOptions IncludeSSN();
        IGeneratorOptions IncludeSSN(bool include);
        IGeneratorOptions IncludeDOB();
        IGeneratorOptions IncludeDOB(bool include);
        IGeneratorOptions IncludeAddress();
        IGeneratorOptions IncludeAddress(bool include);
        IGeneratorOptions SetGenderFilter(GenderFilter filter);
        IGeneratorOptions SetAgeRange(int minimum, int maximum);

        IGenerator CreateGenerator();
    }
}