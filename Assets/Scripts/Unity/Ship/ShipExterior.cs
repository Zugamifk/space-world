using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.Ship
{
    public class ShipExterior : MonoBehaviour
    {
        [SerializeField]
        Rigidbody2D m_Rigidbody;
        [SerializeField]
        PolygonCollider2D m_Collider;

        public Rigidbody2D Rigidbody
        {
            get
            {
                return m_Rigidbody;
            }
        }

        public void SetShape(Vector2[] points)
        {
            m_Collider.points = points;
        }
    }
}