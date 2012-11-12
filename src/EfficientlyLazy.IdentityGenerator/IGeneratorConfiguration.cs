using System.Collections.Generic;
using EfficientlyLazy.IdentityGenerator.Entity;

namespace EfficientlyLazy.IdentityGenerator
{
    /// <summary>
    /// 
    /// </summary>
    public interface IGeneratorConfiguration
    {
        /// <summary>
        /// Includes name generation based on the internal list of names.
        /// </summary>
        /// <returns>
        ///   <see cref="IGeneratorOptions" />
        /// </returns>
        IGeneratorConfiguration IncludeName();
        
        /// <summary>
        /// Includes name generation based on the internal list of names based on the <see cref="GenderFilter"/>.
        /// </summary>
        /// <param name="filter"><see cref="GenderFilter"/></param>
        /// <returns>
        ///   <see cref="IGeneratorOptions" />
        /// </returns>
        IGeneratorConfiguration IncludeName(GenderFilter filter);

        /// <summary>
        /// Includes name generation based on <see cref="INameData"/> provided.
        /// </summary>
        /// <param name="nameData">Name data to use for <see cref="IName"/> generation.</param>
        /// <returns>
        ///   <see cref="IGeneratorOptions" />
        /// </returns>
        IGeneratorConfiguration IncludeName(INameData nameData);

        /// <summary>
        /// Includes SSN generation.
        /// </summary>
        /// <returns>
        ///   <see cref="IGeneratorOptions" />
        /// </returns>
        IGeneratorConfiguration IncludeSSN();

        /// <summary>
        /// Includes SSN generation.
        /// </summary>
        /// <param name="makeDashed">Make the SSNs dashed (xxx-xx-xxxx).</param>
        /// <returns>
        ///   <see cref="IGeneratorOptions" />
        /// </returns>
        IGeneratorConfiguration IncludeSSN(bool makeDashed);

        /// <summary>
        /// Includes SSN generations based on the <see cref="IEnumerable{ISSNAreaCodeData}"/> provided.
        /// </summary>
        /// <param name="ssnAreaCodeData">SSN data to use for SSN generation.</param>
        /// <returns>
        ///   <see cref="IGeneratorOptions" />
        /// </returns>
        IGeneratorConfiguration IncludeSSN(IEnumerable<ISSNAreaCodeData> ssnAreaCodeData);

        /// <summary>
        /// Includes SSN generations based on the <see cref="IEnumerable{ISSNAreaCodeData}" /> provided.
        /// </summary>
        /// <param name="ssnAreaCodeData">SSN data to use for SSN generation.</param>
        /// <param name="makeDashed">Make the SSNs dashed (xxx-xx-xxxx).</param>
        /// <returns>
        ///   <see cref="IGeneratorOptions" />
        /// </returns>
        IGeneratorConfiguration IncludeSSN(IEnumerable<ISSNAreaCodeData> ssnAreaCodeData, bool makeDashed);

        /// <summary>
        /// Includes DOB generation using the internal defaults (1-100).
        /// </summary>
        /// <returns>
        ///   <see cref="IGeneratorOptions" />
        /// </returns>
        IGeneratorConfiguration IncludeDOB();

        /// <summary>
        /// Includes DOB generation based on the specified range.
        /// </summary>
        /// <param name="minimum">The minimum age.</param>
        /// <param name="maximum">The maximum age.</param>
        /// <returns>
        ///   <see cref="IGeneratorOptions" />
        /// </returns>
        IGeneratorConfiguration IncludeDOB(int minimum, int maximum);

        /// <summary>
        /// Includes address generation based on the internal address data.
        /// </summary>
        /// <returns>
        ///   <see cref="IGeneratorOptions" />
        /// </returns>
        IGeneratorConfiguration IncludeAddress();

        /// <summary>
        /// Includes address generation based on <see cref="IAddressData"/> provided.
        /// </summary>
        /// <param name="addressData">Address data to use for <see cref="IAddress"/> generation.</param>
        /// <returns>
        ///   <see cref="IGeneratorOptions" />
        /// </returns>
        IGeneratorConfiguration IncludeAddress(IAddressData addressData);

        /// <summary>
        /// Builds the <see cref="IGenerator" /> based on the options specified in the <see cref="IGeneratorOptions" />.
        /// </summary>
        /// <returns>
        ///   <see cref="IGenerator" />
        /// </returns>
        IGenerator Build();
    }
}