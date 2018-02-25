using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Unity.UI
{
    public class ShipNodeConnectionHandle : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public delegate void NodeHandleCallback(ShipNodeConnectionHandle handle, Vector2 position);
        public event NodeHandleCallback OnDragNode;

        public void OnDisable()
        {
            OnDragNode = null;
        }

        // DRAG
        public void OnBeginDrag(PointerEventData eventData)
        {
            OnDragNode.Invoke(this, eventData.position);
            UpdateTransform(eventData.position);
        }

        public void OnDrag(PointerEventData data)
        {
            OnDragNode.Invoke(this, data.position);
            UpdateTransform(data.position);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnDragNode.Invoke(this, eventData.position);
            UpdateTransform(eventData.position);
        }

        void UpdateTransform(Vector2 position)
        {
            var dir = position - (Vector2)transform.position;
            var angle = Vector2.SignedAngle(Vector2.right, dir);
            transform.eulerAngles = new Vector3(0, 0, angle);
            Debug.Log("angle between " + dir + " and " + Vector2.right + " is " + angle);
        }
    }
}