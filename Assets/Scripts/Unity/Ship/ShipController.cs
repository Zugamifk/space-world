using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Unity.Ship
{
    public class ShipController : MonoBehaviour
    {

        // how far off we can be before we stop moving
        const float k_MovementError = 0.01f;

        class MoveOrder
        {
            public Vector2 position;
        }

        [SerializeField]
        Transform m_Root;
        [SerializeField]
        ShipExterior m_Exterior;

        MoveOrder m_CurrentMove;

        public Vector2 position { get; private set; }

        private void Update()
        {
            position = GetPosition();
            if (m_CurrentMove != null)
            {
                Vector2 path = m_CurrentMove.position - position;
                if (path.magnitude > k_MovementError)
                {
                    ThrustTo(m_CurrentMove.position);
                }
                else
                {
                    m_CurrentMove = null;
                    Stop();
                }
            }
        }

        // ===============================
        // Initialization
        // ===============================
        public void SetHull(ShipExterior hull)
        {
            m_Exterior = hull;
            m_Root = m_Exterior.transform;
        }

        // ===============================
        // Controls
        // ===============================
        void ThrustTo(Vector2 destination)
        {
            Vector2 direction = destination - position;
            Vector2 velocity = m_Exterior.Rigidbody.velocity;
            Thrust(direction - velocity);
        }

        public void MoveTo(Vector2 destination)
        {
            m_CurrentMove = new MoveOrder()
            {
                position = destination
            };
        }

        public void Stop()
        {
            m_Exterior.Rigidbody.velocity = Vector2.zero;
        }

        public void Thrust(Vector2 dir)
        {
            m_Exterior.Rigidbody.AddRelativeForce(dir);
        }

        public Vector2 GetPosition()
        {
            return m_Exterior.Rigidbody.transform.position;
        }

    }
}