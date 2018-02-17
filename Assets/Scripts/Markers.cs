using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Markers : MonoBehaviour {

    [SerializeField]
    Transform m_InactiveMarkerRoot;
    [SerializeField]
    Marker m_SelectedObjectMarker;
    [SerializeField]
    Marker m_TargetMarker;
    [SerializeField]
    SpriteRenderer m_PathLine;

    static Markers m_Instance;

    private void Awake()
    {
        if(m_Instance!=null)
        {
            Debug.LogError("Duplicate Markers found!", m_Instance);
            return;
        }
        m_Instance = this;
    }

    public static void Init()
    {
        Select(null);
        MarkPath(Vector2.zero, Vector2.zero);
    }

    public static void Select(GameObject obj)
    {
        if(obj!=null)
        {
            MarkerTarget target = obj.GetComponent<MarkerTarget>();
            if (target != null)
            {
                m_Instance.m_SelectedObjectMarker.SetTarget(target);
            } else
            {
                Debug.LogWarning("Can not select " + obj + "! Object does not have a MarkerTarget component.");
            }
        } else
        {
            Return(m_Instance.m_SelectedObjectMarker);
        }
    }

    public static void MarkPath(Vector2 from, Vector2 position)
    {
        if (position == Vector2.zero)
        {
            m_Instance.m_TargetMarker.SetTarget(null);
        }
        else
        {
            m_Instance.m_TargetMarker.SetTarget(position);
            SpriteRenderer line = m_Instance.m_PathLine;
            Vector2 ray = position - from;
            line.transform.position = from+  ray/ 2;
            float angle = Vector2.SignedAngle(Vector2.up, position);
            line.transform.eulerAngles = Vector3.forward * angle;
            float len = ray.magnitude;
            Vector2 size = line.size;
            size.y = len;
            line.size = size;
        }
    }

    static void Return(Marker marker)
    {
        marker.SetTarget(null);
        marker.transform.SetParent(m_Instance.m_InactiveMarkerRoot);
    } 
}
