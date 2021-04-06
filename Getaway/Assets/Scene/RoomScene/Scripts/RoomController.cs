using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>ルームシーンを管理するクラス/summary>
public class RoomController : MonoBehaviour
{
    void Start()
    {
        // フェードイン
        Fade.Instance.FadeIn();
    }
}
