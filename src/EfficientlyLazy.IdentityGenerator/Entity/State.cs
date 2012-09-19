using System;

namespace EfficientlyLazy.IdentityGenerator.Entity
{
    internal class State : IState
    {
        public string Abbreviation { get; set; }
        public string Name { get; set; }

        public bool Equals(IState other)
        {
            return Abbreviation.Equals(other.Abbreviation, StringComparison.CurrentCultureIgnoreCase)
                   && Name.Equals(other.Name, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}