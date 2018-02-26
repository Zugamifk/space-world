using UnityEngine;
using System.Collections;

namespace Unity.Ship
{
    public class ShipComponent : MonoBehaviour
    {

        [SerializeField]
        SpriteRenderer m_renderer;

        ShipController m_Ship;

        public void Initialize(ShipController ship)
        {
            m_Ship = ship;
        }
    }
}