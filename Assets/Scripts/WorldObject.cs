using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObject : MonoBehaviour {

    [SerializeField]
    Animator m_Animator;
    [SerializeField]
    Transform m_Root;
    [SerializeField]
    Rigidbody2D m_RigidBody;

    public Animator Animator { get
        {
            return m_Animator;
        }
    }

    public Transform Root
    {
        get
        {
            return m_Root;
        }
    }

    public Rigidbody2D RigidBody
    {
        get
        {
            return m_RigidBody;
        }
    }
}
