using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Ship;

namespace Unity.UI
{
    public class ShipNodeInfoPanel : MonoBehaviour
    {
        [SerializeField]
        Text m_Title;
        [SerializeField]
        Text m_XPosition;
        [SerializeField]
        Text m_YPosition;

        [SerializeField]
        GameObject m_HandleInfoRoot;
        [SerializeField]
        Button m_DeleteButton;

        public void Show(Structure.Node node)
        {
            m_Title.text = "Node";
            m_XPosition.text = node.Position.x.ToString("0.000");
            m_YPosition.text = node.Position.y.ToString("0.000");
        }
        public void SelectEdgeHandle(Structure.FrameSection frame)
        {
            var active = frame != null;
            m_HandleInfoRoot.SetActive(active);
        }
    }
}