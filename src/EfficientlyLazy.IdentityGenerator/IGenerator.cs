using System;
using System.Collections.Generic;
using EfficientlyLazy.IdentityGenerator.Entity;

namespace EfficientlyLazy.IdentityGenerator
{
    /// <summary>
    /// Core for Indentity Generation.
    /// </summary>
    public interface IGenerator
    {
        /// <summary>
        /// Includes Name (first, middle, last) in identity generation
        /// </summary>
        /// <value>
        ///   <c>true</c> if [include name]; otherwise, <c>false</c>. (default: false)
        /// </value>
        bool IncludeName { get; }

        /// <summary>
        /// Includes SSN in identity generation
        /// </summary>
        /// <value>
        ///   <c>true</c> if [include SSN]; otherwise, <c>false</c>. (default: false)
        /// </value>
        bool IncludeSSN { get; }

        /// <summary>
        /// Gets a value indicating whether [include SSN dashes].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [include SSN dashes]; otherwise, <c>false</c>.
        /// </value>
        bool IncludeSSNDashes { get; }

        /// <summary>
        /// Includes Date Of Birth in identity generation
        /// </summary>
        /// <value>
        ///   <c>true</c> if [include DOB]; otherwise, <c>false</c>. (default: false)
        /// </value>
        bool IncludeDOB { get; }

        /// <summary>
        /// Includes Address (address line, city, state, zipcode) in identity generation
        /// </summary>
        /// <value>
        ///   <c>true</c> if [include address]; otherwise, <c>false</c>. (default: false)
        /// </value>
        bool IncludeAddress { get; }

        /// <summary>
        /// Includes Male names in identity generation
        /// </summary>
        /// <value>
        /// The <see cref="GenderFilter"/>. (default: Both)
        /// </value>
        GenderFilter Genders { get; }

        /// <summary>
        /// The minimum age use for random ages in identity generation
        /// </summary>
        /// <value>
        /// The minimum age. (default: 1)
        /// </value>
        int MinimumAge { get; }

        /// <summary>
        /// The maximum age use for random ages in identity generation
        /// </summary>
        /// <value>
        /// The maximum age. (default: 100)
        /// </value>
        int MaximumAge { get; }

        /// <summary>
        /// Generates a single identity base on defined settings.
        /// </summary>
        /// <returns>
        /// Single Random <see cref="IIdentity" />
        /// </returns>
        IIdentity Generate();

        /// <summary>
        /// Generates multiple identities base on defined settings.
        /// </summary>
        /// <param name="number">Number of identities to return.</param>
        /// <returns>
        ///   <see cref="IEnumerable{IIdentity}" />
        /// </returns>
        IEnumerable<IIdentity> Generate(int number);

        /// <summary>
        /// Generates a CSV file.
        /// </summary>
        /// <param name="number">The number identities to generate.</param>
        /// <param name="delimiter">The file record delimiter.</param>
        /// <param name="filename">The output filename.</param>
        void GenerateToFile(int number, string delimiter, string filename);

        /// <summary>
        /// Generates a random <see cref="IName" />.
        /// </summary>
        /// <param name="filter">The <see cref="GenderFilter" />.</param>
        /// <returns>
        ///   <see cref="IName" />
        /// </returns>
        IName GenerateName(GenderFilter filter);

        /// <summary>
        /// Generates an SSN.
        /// </summary>
        /// <returns>
        /// An SSN
        /// </returns>
        string GenerateSSN();

        /// <summary>
        /// Generates an SSN.
        /// </summary>
        /// <param name="stateAbbreviation">The state the SSN should be based on.</param>
        /// <returns>
        /// An SSN
        /// </returns>
        string GenerateSSN(string stateAbbreviation);

        /// <summary>
        /// Generates a random <see cref="IAddress" />.
        /// </summary>
        /// <returns>
        ///   <see cref="IAddress" />
        /// </returns>
        IAddress GenerateAddress();

        /// <summary>
        /// Generates a random Date Of Birth.
        /// </summary>
        /// <returns>
        /// A Date Of Birth between the specified age ranges.
        /// </returns>
        DateTime GenerateDOB();
    }
}