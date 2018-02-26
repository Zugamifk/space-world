using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.Ship
{
    public class ShipObjectBuilder : MonoBehaviour
    {
        [SerializeField]
        ShipController m_ShipControllerTemplate;
        [SerializeField]
        ShipExterior m_ExteriorTemplate;

        ObjectPool<ShipController> m_ShipPool;
        ObjectPool<ShipExterior> m_ShipExteriorPool;

        public static ShipObjectBuilder Instance;

        private void Awake()
        {
            Instance = this;
            m_ShipPool = new ObjectPool<ShipController>(m_ShipControllerTemplate);
            m_ShipExteriorPool = new ObjectPool<ShipExterior>(m_ExteriorTemplate);
        }

        public ShipController BuildShip(Game.Ship.Ship ship)
        {
            var result = m_ShipPool.Get();

            var exterior = m_ShipExteriorPool.Get();

            exterior.SetShape(ship.structure.GetOuterHullPoints().ToArray());
            result.transform.SetParent(exterior.transform);

            result.SetHull(exterior);

            return result;
        }
    }
}