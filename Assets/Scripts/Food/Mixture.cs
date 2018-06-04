using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Chemistry
{
    public class Mixture : Substance
    {
        public Mixture(params IIngredient[] ingredients)
        {
            foreach(var i in ingredients)
            {
                AddIngredient(i);    
            }
        }

        void AddIngredient<TIngredient>(TIngredient ingredient) where TIngredient : IIngredient
        {
            foreach(var i in m_Ingredients)
            {
                if(i is TIngredient)
                {
                    i.Add(ingredient.Amount);
                }
            }
        }
    }
}