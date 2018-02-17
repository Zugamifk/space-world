using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldInput : MonoBehaviour {

    const string k_Horizontal = "Horizontal";
    const string k_Vertical = "Vertical";
    const string k_Use = "Use";

    [SerializeField]
    Camera m_Camera;
    [SerializeField]
    Ship m_PlayerShip;
    [SerializeField]
    Player m_Player;

    Mode m_NeutralMode;
    Mode m_PlayerMode;
    Mode m_CurrentMode;

    static WorldInput m_Instance;

    // ===============================
    // MonoBehaviour
    // ===============================
    void Awake()
    {
        if(m_Instance!=null)
        {
            Debug.LogError("More than one instance of WorldInput found!");
            return;
        }
        m_Instance = this;
        m_NeutralMode = new NeutralMode();
        m_PlayerMode = new PlayerMode() { m_Player = m_Player };
        m_CurrentMode = m_PlayerMode;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousepos = m_Camera.ScreenToWorldPoint(Input.mousePosition);
            m_CurrentMode.OnClick(mousepos);
        }
        m_CurrentMode.UpdateAxes();
    }

    // ===============================
    // Public Static
    // ===============================

    public static void InitMove()
    {
        m_Instance.m_CurrentMode = new MoveMode();
    }

    // ===============================
    // Modes
    // ===============================

    void ResetMode()
    {
        m_CurrentMode = m_NeutralMode;
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

    // use for ship control
    class MoveMode : Mode
    {
        public override void OnClick(Vector2 position)
        {
            Debug.Log("Move to " + position);
            Markers.MarkPath(m_Instance.m_PlayerShip.position, position);
            m_Instance.m_PlayerShip.MoveTo(position);
            m_Instance.ResetMode();
        }
    }
}
