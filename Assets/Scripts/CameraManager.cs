using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    [SerializeField]
    Camera m_PlayerCamera;

    static CameraManager m_Instance;
    List<ICameraUser> m_CurrentUsers = new List<ICameraUser>();

    // ===============================
    // MonoBehaviour
    // ===============================
    void Awake()
    {
        if (m_Instance != null)
        {
            Debug.LogError("More than one instance of CameraManager found!");
            return;
        }
        m_Instance = this;

        var player = FindObjectOfType<Player>();
        player.AcquireCamera(m_PlayerCamera);
        m_CurrentUsers.Add(player);
    }
}
