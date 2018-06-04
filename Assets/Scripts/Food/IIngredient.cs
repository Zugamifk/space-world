using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents an item that can be combined in a mixture
/// </summary>
namespace Game.Chemistry
{
    public interface IIngredient { 
    
        /// <summary>
        /// ingredients the make up this ingredient
        /// </summary>
        IEnumerable<IIngredient> SubIngredients { get; }

        /// <summary>
        /// total chemicals in this ingredint
        /// </summary>
        IEnumerable<Element> Chemicals { get; }

        void Add(float amount);

        /// <summary>
        /// How much of this ingredient there is, in grams
        /// </summary>
        float Amount { get; }

        /// <summary>
        /// name of this ingredient
        /// </summary>
        string Name { get; }

    }
}