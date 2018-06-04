using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Chemistry
{
    /// <summary>
    /// representrs a molecule made of elementary atoms
    /// </summary>
    public partial class ElementCompound : Compound
    {
        class AtomCount
        {
            public Element element;
            public int count;
        }
        Dictionary<string, AtomCount> m_Elements;
        public ElementCompound()
        {
            m_Elements = new Dictionary<string, AtomCount>();
        }

        public ElementCompound Add(Element element, int count = 1)
        {
            AtomCount contained;
            if(m_Elements.TryGetValue(element.Name, out contained))
            {
                contained.count += count;
            } else
            {
                var ac = new AtomCount()
                {
                    element = element,
                    count = count
                };
                m_Elements.Add(element.Name, ac);
            }
            
            RegenerateName();
            return this;
        }

        protected override void RegenerateName()
        {
            var sb = new System.Text.StringBuilder();
            foreach (var i in m_Elements.Values)
            {
                sb.Append(i.element.ShortName);
                if(i.count > 1)
                {
                    sb.Append(i.count);
                }
            }
            m_Name = FindChemicalName(sb.ToString());
        }
    }
}
