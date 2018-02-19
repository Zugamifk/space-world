using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour {

    [SerializeField]
    SpriteRenderer m_Sprite;
    [SerializeField]
    Vector2 m_Padding;

    private void Awake()
    {
        m_Sprite.enabled = false;
    }

    public void SetActive(bool active)
    {
        m_Sprite.enabled = active;
    }

    public void SetSize(Vector2 size)
    {
        size += m_Padding;
        m_Sprite.size = size;
    }

    public void SetTarget(MarkerTarget target)
    {
        if (target != null)
        {
            SetSize(target.GetSize());
            transform.SetParent(target.transform);
            transform.localPosition = Vector2.zero;
            SetActive(true);
        } else
        {
            transform.SetParent(null);
            SetActive(false);
        }
    }

    public void SetTarget(Vector2 position)
    {
        SetSize(Vector2.one);
        transform.localPosition = position;
        SetActive(true);
    }
}
