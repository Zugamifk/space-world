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
            m_Button.onClick.AddListener(OnClick);
        }

        void OnClick()
        {

        }
    }
}