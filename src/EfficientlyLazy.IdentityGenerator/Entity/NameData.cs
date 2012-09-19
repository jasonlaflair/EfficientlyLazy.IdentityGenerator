using System.Collections.Generic;

namespace EfficientlyLazy.IdentityGenerator.Entity
{
    internal class NameData : INameData
    {
        public GenderFilter GenderFilter { get; set; }
        public IEnumerable<IFirstNameData> FirstNameData { get; set; }
        public IEnumerable<string> LastNameData { get; set; }
    }
}