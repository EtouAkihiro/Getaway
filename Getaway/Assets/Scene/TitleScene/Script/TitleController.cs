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

    // タイマー
    float m_Time = 0.0f;

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
        // 1秒ごとにロビーに接続
        if(m_TitleCanvas.m_SceneState == TitleCanvas.SceneState.Select && m_Time >= 1.0f)
        {
            // ロビーに接続
            PhotonManager.Instance.JointLobby();
            // タイマーリセット
            m_Time = 0.0f;
        }
        else
        {
            // 時間経過
            m_Time += Time.deltaTime;
        }
    }
}
