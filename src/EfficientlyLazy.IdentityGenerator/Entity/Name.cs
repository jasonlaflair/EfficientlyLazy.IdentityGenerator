namespace EfficientlyLazy.IdentityGenerator.Entity
{
    internal class Name : IName
    {
        public string First { get; set; }
        public string Middle { get; set; }
        public string Last { get; set; }
        public Gender Gender { get; set; }
    }
}