using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Chemistry
{
    // represents a combination of chemicals, as produced by a mixture
    public class Compound
    {
        protected string m_Identifier;
        protected string m_Name;
        protected Dictionary<string, Compound> m_Ingredients;
        protected float m_Mass;

        public string Identifier
        {
            get
            {
                return m_Identifier;
            }
        }

        public string Name
        {
            get
            {
                return m_Name;
            }
        }

        public IEnumerable<Compound> Ingredients
        {
            get
            {
                return m_Ingredients.Values;
            }
        }

        public float Mass
        {
            get
            {
                return m_Mass;
            }
        }

        protected Compound(string identifier, float mass = 0)
        {
            m_Identifier = identifier;
            m_Name = identifier;
            m_Ingredients = new Dictionary<string, Compound>();
            m_Mass = mass;
        }

        public Compound(params Compound[] others)
            : this(string.Empty)
        {
            foreach(var i in others)
            {
                Add(i);
            }
            m_Identifier = m_Name;
        }

        public static Compound Null()
        {
            return new Compound("NULL");
        }

        public virtual Compound Add(Compound other)
        {
            //Compound contained;
            //if(other.Identifier == Identifier) {
            //    foreach(var i in other.Ingredients)
            //    {
            //        m_Ingredients[i.Identifier].Add(i);
            //    }
            //} else if (m_Ingredients.TryGetValue(other.Identifier, out contained))
            //{
            //    contained.Add(other);
            //}
            //else
            //{
            //    m_Ingredients.Add(other.Identifier, other);
            //}
            //m_Mass += other.Mass;
            //RegenerateName();
            //return this;
            return other;
        }

        protected virtual void RegenerateName()
        {
            var sb = new System.Text.StringBuilder();
            foreach(var i in m_Ingredients.Values)
            {
                sb.Append("[");
                sb.Append(i.Name);
                sb.Append("] + ");
            }
            sb.Length -= 3;
            m_Name = sb.ToString();
        }
    }
}
