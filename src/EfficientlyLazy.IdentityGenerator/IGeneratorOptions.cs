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
        IGeneratorOptions IncludeGenderMale();
        IGeneratorOptions IncludeGenderMale(bool include);
        IGeneratorOptions IncludeGenderFemale();
        IGeneratorOptions IncludeGenderFemale(bool include);
        IGeneratorOptions IncludeGenderBoth();
        IGeneratorOptions SetAgeRange(int minimum, int maximum);

        Generator CreateGenerator();
    }
}