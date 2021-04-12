using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>地下のステージのシーンの管理</summary>
public class UndergroundGameController : MonoBehaviour
{
    void Start()
    {
        // フェイドインする。
        Fade.Instance.FadeIn();
    }
}
