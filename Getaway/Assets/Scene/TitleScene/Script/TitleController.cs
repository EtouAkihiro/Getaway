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

    /// <summary>定期的に接続する時間を計る</summary>
    float m_JointLobbyTime = 0;


    void Start()
    {
        // タイトルキャンバスを取得
        m_TitleCanvas = m_TitleCanvasObject.GetComponent<TitleCanvas>();
        // フェードイン
        Fade.Instance.FadeIn();
    }

    void Update()
    {
        // 経過時間が5秒以上で、ゲームスタートボタンが押された状態だった場合
        // ロビーに接続
        if(m_JointLobbyTime >= 5　&& m_TitleCanvas.GameStartButtonClickFrag)
        {
            // ロビーに接続
            PhotonManager.Instance.JointLobby();
            // タイマーリセット
            m_JointLobbyTime = 0.0f;
        }
        else
        {
            // 時間経過
            m_JointLobbyTime += Time.deltaTime;
        }
    }
}
