using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPoint : MonoBehaviour
{
    public GameObject m_PlayerCamera;

    void Start()
    {
        // 自身の位置に設定
        m_PlayerCamera.transform.position = transform.position;
    }
}
