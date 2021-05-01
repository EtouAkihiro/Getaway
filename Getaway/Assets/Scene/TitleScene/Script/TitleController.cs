using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Chat;

/// <summary>タイトルシーンの管理</summary>
public class TitleController : MonoBehaviour
{
    /// <summary>タイトルキャンバスのオブジェクト</summary>
    public GameObject m_TitleCanvasObject;

    /// <summary>タイトルキャンバスクラス</summary>
    TitleCanvas m_TitleCanvas;


    void Start()
    {
        // タイトルキャンバスを取得
        m_TitleCanvas = m_TitleCanvasObject.GetComponent<TitleCanvas>();
        // フェードイン
        Fade.Instance.FadeIn();
    }

    void Update()
    {
        // 現在のシーンがセレクトシーンだった場合
        // ロビーに接続
        if(m_TitleCanvas.m_SceneState == TitleCanvas.SceneState.Select)
        {
            // ロビーに接続
            PhotonManager.Instance.JointLobby();
        }
    }
}
