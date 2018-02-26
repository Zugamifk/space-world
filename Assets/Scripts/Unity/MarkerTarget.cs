using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerTarget : MonoBehaviour {

    [SerializeField]
    Collider2D m_Collider;

    public Vector2 GetSize()
    {
        return m_Collider.bounds.size;
    }
}
