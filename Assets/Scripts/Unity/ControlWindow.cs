using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.UI
{
    public class ControlWindow : MonoBehaviour
    {

        [SerializeField]
        Button m_MovementButton;

        [SerializeField]
        Ship.ShipController m_Ship;

        public void MovementButton_OnClick()
        {
            WorldInput.Instance.InitMove();
        }

        public void BuildButton_OnClick()
        {
            UIManager.Instance.ShowShipBuildingScreen();
        }
    }
}