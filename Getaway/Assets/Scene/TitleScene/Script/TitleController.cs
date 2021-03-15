using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>タイトルシーンの管理</summary>
public class TitleController : MonoBehaviour
{
    void Start()
    {
        // フェードイン
        Fade.Instance.FadeIn();
    }
}
