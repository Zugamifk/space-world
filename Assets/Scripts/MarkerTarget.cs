using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerTarget : MonoBehaviour {

    [SerializeField]
    BoxCollider2D m_Collider;

    public Vector2 GetSize()
    {
        return m_Collider.size;
    }
}
