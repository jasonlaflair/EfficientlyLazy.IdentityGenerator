namespace EfficientlyLazy.IdentityGenerator.Entity
{
    internal class SSNAreaCodeData : ISSNAreaCodeData
    {
        public int Minimum { get; set; }
        public int Maximum { get; set; }
        public string StateAbbreviation { get; set; }
    }
}