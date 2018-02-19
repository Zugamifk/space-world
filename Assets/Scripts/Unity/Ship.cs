using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Unity
{
    public class Ship : MonoBehaviour
    {

        // how far off we can be before we stop moving
        const float k_MovementError = 0.01f;

        class MoveOrder
        {
            public Vector2 position;
        }

        [SerializeField]
        Rigidbody2D m_Body;
        [SerializeField]
        List<ShipComponent> m_Components;

        MoveOrder m_CurrentMove;

        public Vector2 position { get; private set; }

        void Awake()
        {
            for (int i = 0; i < m_Components.Count; i++)
            {
                m_Components[i].Initialize(this);
            }
        }

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

        void ThrustTo(Vector2 destination)
        {
            Vector2 direction = destination - position;
            Vector2 velocity = m_Body.velocity;
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
            m_Body.velocity = Vector2.zero;
        }

        public void Thrust(Vector2 dir)
        {
            m_Body.AddRelativeForce(dir);
        }

        public Vector2 GetPosition()
        {
            return m_Body.transform.position;
        }

    }
}