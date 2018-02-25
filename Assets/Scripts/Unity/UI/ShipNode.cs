using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.UI
{
    [RequireComponent(typeof(Button))]
    public class ShipNode : MonoBehaviour
    {
        Button m_Button;
        private void Awake()
        {
            m_Button = GetComponent<Button>();
        }

        public void SetOnClick(UnityEngine.Events.UnityAction onClick)
        {
            m_Button.onClick.AddListener(onClick);
        }
    }
}