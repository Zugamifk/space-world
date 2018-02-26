using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Unity.UI
{
    [RequireComponent(typeof(Button))]
    public class ShipNode : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public delegate void PositionCallback(ShipNode node, Vector2 position);
        public event PositionCallback OnDragNode;

        Button m_Button;
        private void Awake()
        {
            m_Button = GetComponent<Button>();
        }

        public void SetOnClick(UnityEngine.Events.UnityAction onClick)
        {
            m_Button.onClick.AddListener(onClick);
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            OnDragNode?.Invoke(this, eventData.position);
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            OnDragNode?.Invoke(this, eventData.position);
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            OnDragNode?.Invoke(this, eventData.position);
        }
    }
}