using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Chemistry
{
    public partial class ElementCompound
    {
        static Dictionary<string, string> s_ChemicalNames = new Dictionary<string, string>()
        {
            {"H2O", "Water"}
        };
        protected string FindChemicalName(string name)
        {
            string newName;
            if(s_ChemicalNames.TryGetValue(name, out newName))
            {
                return newName;
            } else
            {
                return name;
            }
        }
    }
}