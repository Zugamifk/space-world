using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Chemistry
{
    /// <summary>
    /// represents an atomic building block for chemistry, such as elements and other basic chemicals
    /// </summary>
    public partial struct Element
    {
        public readonly string Name;
        public readonly string ShortName;
        public Element(string name, string shortName)
        {
            Name = name;
            ShortName = shortName;
        }

        public override bool Equals(object obj)
        {
            var chemical = (Element)obj;
            return Name == chemical.Name;
        }

        public override int GetHashCode()
        {
            var hashCode = 1148058975;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            return hashCode;
        }

        public static bool operator ==(Element a, Element b) => a.Name == b.Name;
        public static bool operator !=(Element a, Element b) => a.Name != b.Name;
    }
}