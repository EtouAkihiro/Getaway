using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

/// <summary>タイトルシーンの管理</summary>
public class TitleController : MonoBehaviour
{
    void Start()
    {
        // フェードイン
        Fade.Instance.FadeIn();
    }
}
