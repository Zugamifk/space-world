using UnityEngine;
using System.Collections;

namespace Unity
{
    public class ShipComponent : MonoBehaviour
    {

        [SerializeField]
        SpriteRenderer m_renderer;

        Ship m_Ship;

        public void Initialize(Ship ship)
        {
            m_Ship = ship;
        }
    }
}