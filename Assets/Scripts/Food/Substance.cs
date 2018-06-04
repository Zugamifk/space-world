using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Chemistry
{
    /// <summary>
    /// The product of a mixture
    /// TODO: implement a base class for the world object this would be
    /// </summary>
    public class Substance : IIngredient
    {
        protected List<IIngredient> m_Ingredients = new List<IIngredient>();
        public IEnumerable<IIngredient> SubIngredients
        {
            get
            {
                return m_Ingredients;
            }
        }

        protected List<Element> m_Chemicals = new List<Element>();
        public IEnumerable<Element> Chemicals
        {
            get
            {
                return m_Chemicals;
            }
        }

        protected float m_Amount;
        public float Amount
        {
            get
            {
                return m_Amount;
            }
        }

        protected string m_Name;
        public string Name
        {
            get
            {
                return m_Name;
            }
        }

        public void Add(float amount)
        {
            m_Amount += amount;
        }
    }
}