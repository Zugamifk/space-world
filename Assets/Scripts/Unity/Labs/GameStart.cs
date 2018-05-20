using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Lab;

namespace Unity
{
    public class GameStart : MonoBehaviour
    {
        MapController m_MapControl;

        private void Awake()
        {
            m_MapControl = new MapController();
            m_MapControl.GenerateMap();
        }

    }
}