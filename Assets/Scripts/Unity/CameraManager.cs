using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity
{
    public class CameraManager : MonoBehaviour
    {

        [SerializeField]
        Camera m_PlayerCamera;

        public static CameraManager Instance;
        List<ICameraUser> m_CurrentUsers = new List<ICameraUser>();

        // ===============================
        // MonoBehaviour
        // ===============================
        void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("More than one instance of CameraManager found!");
                return;
            }
            Instance = this;

            var player = FindObjectOfType<Player>();
            player.AcquireCamera(m_PlayerCamera);
            m_CurrentUsers.Add(player);
        }
    }
}