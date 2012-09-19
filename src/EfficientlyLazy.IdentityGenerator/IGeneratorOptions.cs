using System.Collections.Generic;
using EfficientlyLazy.IdentityGenerator.Entity;

namespace EfficientlyLazy.IdentityGenerator
{
    /// <summary></summary>
    public interface IGeneratorOptions
    {
        /// <summary></summary>
        IGeneratorOptions IncludeName();
        /// <summary></summary>
        IGeneratorOptions IncludeName(GenderFilter filter);
        /// <summary></summary>
        IGeneratorOptions IncludeName(INameData nameData);
        /// <summary></summary>
        IGeneratorOptions ExcludeName();

        /// <summary></summary>
        IGeneratorOptions IncludeSSN();
        /// <summary></summary>
        IGeneratorOptions IncludeSSN(IEnumerable<ISSNAreaCodeData> ssnAreaCodeData);
        /// <summary></summary>
        IGeneratorOptions ExcludeSSN();

        /// <summary></summary>
        IGeneratorOptions IncludeDOB();
        /// <summary></summary>
        IGeneratorOptions IncludeDOB(int minimum, int maximum);
        /// <summary></summary>
        IGeneratorOptions ExcludeDOB();

        /// <summary></summary>
        IGeneratorOptions IncludeAddress();
        /// <summary></summary>
        IGeneratorOptions IncludeAddress(IAddressData addressData);
        /// <summary></summary>
        IGeneratorOptions ExcludeAddress();

        /// <summary></summary>
        IGenerator Build();
    }
}