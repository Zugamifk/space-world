using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        Canvas m_Canvas;
        [SerializeField]
        ShipBuildingScreen m_ShipBuildingScreen;

        public static UIManager Instance;


        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("More than one instance of UIManager found!");
                return;
            }
            Instance = this;
        }

        public void ShowShipBuildingScreen()
        {
            m_ShipBuildingScreen.SetActive(true);
        }

    }
}