using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Unity
{
    public class WorldInput : MonoBehaviour
    {

        const string k_Horizontal = "Horizontal";
        const string k_Vertical = "Vertical";
        const string k_Use = "Use";
        const string k_Alt = "Alt";
        const string k_Mouse1 = "Alt";
        const string k_Mouse0 = "Use";

        public class InputState
        {
            public float x;
            public float y;
            public bool use;
            public bool alt;
        }

        [SerializeField]
        Camera m_Camera;
        [SerializeField]
        Ship m_PlayerShip;
        [SerializeField]
        Player m_Player;

        Mode m_NeutralMode;
        Mode m_PlayerMode;
        CustomMode m_CustomMode;
        Mode m_CurrentMode;

        public static WorldInput Instance;

        // ===============================
        // MonoBehaviour
        // ===============================
        void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("More than one instance of WorldInput found!");
                return;
            }
            Instance = this;
            m_NeutralMode = new NeutralMode();
            m_PlayerMode = new PlayerMode() { m_Player = m_Player };
            m_CustomMode = new CustomMode();
            m_CurrentMode = m_PlayerMode;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                m_CurrentMode.OnClick(Input.mousePosition);
            }
            m_CurrentMode.UpdateAxes();
        }

        // ===============================
        // Public
        // ===============================

        public void InitMove()
        {
            m_CurrentMode = new MoveMode();
        }

        public void SetCustomMode(System.Action<InputState> inputCallback)
        {
            var cm = m_CustomMode;
            cm.OnUpdateInput = inputCallback;
            cm.ResetMode = m_CurrentMode;
            SetMode(cm);
        }

        public void EndCustomMode()
        {
            if (m_CurrentMode == m_CustomMode)
            {
                SetMode(m_CustomMode.ResetMode);
            }
        }

        // ===============================
        // Modes
        // ===============================
        void ResetMode()
        {
            m_CurrentMode = m_NeutralMode;
        }

        void SetMode(Mode mode)
        {
            m_CurrentMode = mode;
        }

        class Mode
        {
            public virtual void OnClick(Vector2 position) { }
            public virtual void UpdateAxes() { }
        }

        class NeutralMode : Mode
        {
            public override void OnClick(Vector2 position)
            {
                position = Instance.m_Camera.ScreenToWorldPoint(position);
                RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero);
                if (hit.collider != null)
                {
                    Debug.Log("clicked " + hit.collider.gameObject.name);
                    Markers.Select(hit.collider.gameObject);
                }
                else
                {
                    Markers.Select(null);
                }
            }
        }

        class PlayerMode : Mode
        {
            public Player m_Player;
            public override void UpdateAxes()
            {
                var x = Input.GetAxis(k_Horizontal);
                var y = Input.GetAxis(k_Vertical);
                var use = Input.GetAxis(k_Use) > 0;
                Player.ControlInput.EAction action =
                    use ? Player.ControlInput.EAction.Use :
                    Player.ControlInput.EAction.None;
                m_Player.UpdateState(new Player.ControlInput()
                {
                    Movement = new Vector2(x, y),
                    Action = action
                });
            }
        }

        class CustomMode : Mode
        {
            public System.Action<InputState> OnUpdateInput;
            public Mode ResetMode;
            public override void UpdateAxes()
            {
                OnUpdateInput.Invoke(
                    new InputState()
                    {
                        x = Input.GetAxis(k_Horizontal),
                        y = Input.GetAxis(k_Vertical),
                        use = Input.GetAxis(k_Use) > 0,
                        alt = Input.GetMouseButtonUp(1)
                    }
                   );
            }
        }

        // use for ship control
        class MoveMode : Mode
        {
            public override void OnClick(Vector2 position)
            {
                position = Instance.m_Camera.ScreenToWorldPoint(position);

                Debug.Log("Move to " + position);
                Markers.MarkPath(Instance.m_PlayerShip.position, position);
                Instance.m_PlayerShip.MoveTo(position);
                Instance.ResetMode();
            }
        }
    }
}