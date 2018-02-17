using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ICameraUser
{

    const string k_IdleAnim = "Idle";
    const string k_UseAnim = "Use";
    const string k_RifleAnim = "Gun";

    // input from player controls
    public class ControlInput
    {
        public enum EAction
        {
            None,
            Use
        };
        public Vector2 Movement;
        public EAction Action;
    }

    [SerializeField]
    WorldObject m_WorldObject;

    IdleState m_IdleState;
    UseState m_UseState;
    State m_CurrentState;

    Camera m_Camera;

    public float Speed { get { return 50; } }

    public Camera Camera { get { return m_Camera; } }

    class State
    {
        protected Player m_Player;
        protected string m_AnimationState;
        public State(Player player)
        {
            m_Player = player;
        }
        public virtual void OnEnterState()
        {
            m_Player.m_WorldObject.Animator.SetTrigger(m_AnimationState);
        }
        public virtual void OnExitState()
        {

        }
        public virtual void Tick()
        {

        }
        public virtual void UseInput(ControlInput input) { }
    }

    class IdleState : State
    {
        public IdleState(Player player) : base(player)
        {
            m_AnimationState = k_IdleAnim;
        }
        public override void UseInput(ControlInput input)
        {
            switch (input.Action)
            {
                case ControlInput.EAction.Use:
                    {
                        m_Player.SetUseState();
                        return;
                    }
                default: break;
            }
            if (input.Movement != Vector2.zero)
            {
                m_Player.Move(input.Movement);
                var angle = Vector2.Angle(Vector2.up, input.Movement);
                m_Player.Rotate(angle);
            }
        }
    }

    class UseState : State
    {
        public UseState(Player player) : base(player)
        {
            m_AnimationState = k_UseAnim;
        }
    }

    private void Awake()
    {
        m_IdleState = new IdleState(this);
        m_UseState = new UseState(this);
        SetState(m_IdleState);
    }

    void SetState(State state)
    {
        if (m_CurrentState != null)
        {
            m_CurrentState.OnExitState();
        }

        m_CurrentState = state;
        m_CurrentState.OnEnterState();
    }

    void SetIdleState()
    {
        SetState(m_IdleState);
    }

    void SetUseState()
    {
        SetState(m_UseState);
    }

    void Move(Vector2 direction)
    {
        m_WorldObject.RigidBody.MovePosition((Vector2)m_WorldObject.Root.position + direction * Speed * Time.fixedDeltaTime);
        Debug.Log("Moving " + direction);
    }

    void Rotate(float angle)
    {
        m_WorldObject.Renderer.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void Update()
    {
        if (m_CurrentState != null)
        {
            m_CurrentState.Tick();
        }
    }

    public void UpdateState(ControlInput input)
    {
        m_CurrentState.UseInput(input);
    }

    public void AcquireCamera(Camera camera)
    {
        m_Camera = camera;
        camera.transform.SetParent(m_WorldObject.Root);
    }
}
