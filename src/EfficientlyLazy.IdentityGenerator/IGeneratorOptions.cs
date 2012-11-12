using System.Collections.Generic;
using EfficientlyLazy.IdentityGenerator.Entity;

namespace EfficientlyLazy.IdentityGenerator
{
    /// <summary>
    /// 
    /// </summary>
    public interface IGeneratorOptions
    {
        /// <summary>
        /// Gets a value indicating whether [name included].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [name included]; otherwise, <c>false</c>.
        /// </value>
        bool NameIncluded { get; }

        /// <summary>
        /// Gets a value indicating whether [SSN included].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [SSN included]; otherwise, <c>false</c>.
        /// </value>
        bool SSNIncluded { get; }

        /// <summary>
        /// Gets a value indicating whether [SSN dashed].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [SSN dashed]; otherwise, <c>false</c>.
        /// </value>
        bool SSNDashed { get; }

        /// <summary>
        /// Gets a value indicating whether [DOB included].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [DOB included]; otherwise, <c>false</c>.
        /// </value>
        bool DOBIncluded { get; }

        /// <summary>
        /// Gets a value indicating whether [address include].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [address include]; otherwise, <c>false</c>.
        /// </value>
        bool AddressInclude { get; }

        /// <summary>
        /// Gets the gender filter.
        /// </summary>
        /// <value>
        /// The gender filter.
        /// </value>
        GenderFilter GenderFilter { get; }

        /// <summary>
        /// Gets the minimum age.
        /// </summary>
        /// <value>
        /// The minimum age.
        /// </value>
        int MinimumAge { get; }
        
        /// <summary>
        /// Gets the maximum age.
        /// </summary>
        /// <value>
        /// The maximum age.
        /// </value>
        int MaximumAge { get; }

        /// <summary>
        /// Gets the external name data.
        /// </summary>
        /// <value>
        /// The external name data.
        /// </value>
        INameData ExternalNameData { get; }
        
        /// <summary>
        /// Gets the external address data.
        /// </summary>
        /// <value>
        /// The external address data.
        /// </value>
        IAddressData ExternalAddressData { get; }

        /// <summary>
        /// Gets the external SSN area code data.
        /// </summary>
        /// <value>
        /// The external SSN area code data.
        /// </value>
        IEnumerable<ISSNAreaCodeData> ExternalSSNAreaCodeData { get; }
    }
}