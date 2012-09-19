using System;

namespace EfficientlyLazy.IdentityGenerator.Entity
{
    internal class SSNAreaCodeData : ISSNAreaCodeData
    {
        public int Minimum { get; set; }
        public int Maximum { get; set; }
        public string StateAbbreviation { get; set; }
        
        public bool Equals(ISSNAreaCodeData other)
        {
            return StateAbbreviation.Equals(other.StateAbbreviation, StringComparison.CurrentCultureIgnoreCase)
                   && Minimum.Equals(other.Minimum)
                   && Maximum.Equals(other.Maximum);
        }
    }
}