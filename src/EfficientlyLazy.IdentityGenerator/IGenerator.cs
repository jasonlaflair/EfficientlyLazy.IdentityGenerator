using System;
using System.Collections.Generic;
using EfficientlyLazy.IdentityGenerator.Entity;

namespace EfficientlyLazy.IdentityGenerator
{
    /// <summary></summary>
    public interface IGenerator
    {
        /// <summary>Includes Name (first, middle, last) in identity generation (default: false)</summary>
        bool IncludeName { get; }

        /// <summary>Includes SSN in identity generation (default: false)</summary>
        bool IncludeSSN { get; }

        /// <summary>Includes Date Of Birth in identity generation (default: false)</summary>
        bool IncludeDOB { get; }

        /// <summary>Includes Address (address line, city, state, zipcode) in identity generation (default: false)</summary>
        bool IncludeAddress { get; }

        /// <summary>Includes Male names in identity generation (default: false)</summary>
        GenderFilter Genders { get; }
        
        /// <summary>The minimum age use for random ages in identity generation (default: 1)</summary>
        int MinimumAge { get; }
        
        /// <summary>The maximum age use for random ages in identity generation (default: 100)</summary>
        int MaximumAge { get; }

        /// <summary>Generates a single identity base on defined settings.</summary>
        /// <returns>Single Random <see cref="Identity"/></returns>
        IIdentity Generate();
        
        /// <summary>Generates multiple identities base on defined settings.</summary>
        /// <param name="number">Number of identities to return.</param>
        /// <returns><see cref="IEnumerable{Identity}"/></returns>
        IEnumerable<IIdentity> Generate(int number);
        
        /// <summary></summary>
        void Generate(int number, string delimiter, string filename);

        /// <summary></summary>
        IName GenerateName(GenderFilter filter);

        /// <summary></summary>
        string GenerateSSN();

        /// <summary></summary>
        string GenerateSSN(string stateAbbreviation);

        /// <summary></summary>
        IAddress GenerateAddress();

        /// <summary></summary>
        DateTime GenerateDOB();

        /// <summary></summary>
        DateTime GenerateDOB(int youngestAge, int oldestAge);
    }
}