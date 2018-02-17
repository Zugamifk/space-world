using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlWindow : MonoBehaviour {

    [SerializeField]
    Button m_MovementButton;

    [SerializeField]
    Ship m_Ship;

    public void MovementButton_OnClick()
    {
        WorldInput.InitMove();
    }
}
