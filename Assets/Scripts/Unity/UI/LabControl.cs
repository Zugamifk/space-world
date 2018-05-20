using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.UI
{
    public class LabControl : MonoBehaviour
    {
        [SerializeField]
        GameController m_GameController;

        public void RebuildButton_OnClick()
        {
            m_GameController.RebuildMap();
        }

        public void Reset_OnClick()
        {
            m_GameController.ResetMap();
        }

        public void NextIter_OnClick()
        {
            m_GameController.StepMapIteration();
        }
    }
}