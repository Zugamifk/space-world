using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Chemistry
{
    public partial struct Element
    {
        public const int ElementsCount = 4;
        public static Element GetIndexed(int index)
        {
            Element result;
            switch (index)
            {
                case 0: return Hydrogen();
                case 1: return Oxygen();
                case 2: return Carbon();
                case 3: return Sulfur();
                default: result = default(Element);
                    break;
            }
            return result;
        }

        public const string HydrogenName = "Hydrogen";
        public const string HydrodenNameShort = "H";
        public static Element Hydrogen()
        {
            return new Element(HydrogenName, HydrodenNameShort);
        }

        public const string OxygenName = "Oxygen";
        public const string OxygenNameShort = "O";
        public static Element Oxygen()
        {
            return new Element(OxygenName, OxygenNameShort);
        }

        public const string CarbonName = "Carbon";
        public const string CarbonNameShort = "C";
        public static Element Carbon()
        {
            return new Element(CarbonName, CarbonNameShort);
        }

        public const string SulfurName = "Sulfur";
        public const string SulfurNameShort = "S";
        public static Element Sulfur()
        {
            return new Element(SulfurName, SulfurNameShort);
        }
    }
}