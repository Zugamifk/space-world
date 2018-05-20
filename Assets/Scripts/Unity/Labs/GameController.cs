using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Lab;
using Unity.Lab;

namespace Unity
{
    public class GameController : MonoBehaviour
    {
        [SerializeField]
        MapView m_MapView;

        MapController m_MapControl;

        private void Awake()
        {
            m_MapControl = new MapController();
            m_MapControl.GenerateMap();
        }

        public void RebuildMap()
        {
            m_MapControl.GenerateMap();
            m_MapView.ResetInitState();
        }
    }
}