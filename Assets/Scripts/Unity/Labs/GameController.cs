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

        MapModel m_MapModel;
        MapController m_MapControl;

        private void Awake()
        {
            m_MapControl = new MapController();
            m_MapControl.GenerateMap();
            m_MapView.Refresh(m_MapControl.CurrentMap);
        }

        public void RebuildMap()
        {
            m_MapControl.GenerateMap();
            m_MapView.Refresh(m_MapControl.CurrentMap);
        }

        public void ResetMap()
        {
            m_MapControl.ResetMap();
            m_MapView.Refresh(m_MapControl.CurrentMap);
        }

        public void StepMapIteration()
        {
            m_MapControl.StepMapIteration();
            m_MapView.Refresh(m_MapControl.CurrentMap);
        }
    }
}