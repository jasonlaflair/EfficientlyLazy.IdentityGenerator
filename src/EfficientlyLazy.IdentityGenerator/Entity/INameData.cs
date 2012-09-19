using System.Collections.Generic;

namespace EfficientlyLazy.IdentityGenerator.Entity
{
    /// <summary></summary>
    public interface INameData
    {
        /// <summary></summary>
        GenderFilter GenderFilter { get; }

        /// <summary></summary>
        IEnumerable<IFirstNameData> FirstNameData { get; }

        /// <summary></summary>
        IEnumerable<string> LastNameData { get; }
    }
}