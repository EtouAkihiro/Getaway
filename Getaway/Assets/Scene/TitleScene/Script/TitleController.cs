using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class TitleController : MonoBehaviour
{
    /// <summary>ゲームスタートボタン</summary>
    GameObject m_GameStartButton;
    /// <summary>ゲームスタートボタンのスクリプトの参照</summary>
    GameStartButton m_GameStartButtonScript;

    /// <summary>タイトルキャンバスのスクリプトの参照</summary>
    TitleCanvas m_TitleCanvasScript;

    void Start()
    {
        // ゲームスタートボタンを取得
        m_GameStartButton = GameObject.Find("GameStartButton");
        // ゲームスタートボタンのスクリプトの取得
        m_GameStartButtonScript = m_GameStartButton.GetComponent<GameStartButton>();
        // タイトルキャンバスのスクリプトの取得
        m_TitleCanvasScript = GameObject.Find("TitleCanvas").GetComponent<TitleCanvas>();
        // フェードイン
        Fade.Instance.FadeIn();
    }

    /// <summary> ゲームスタートボタンが押された時</summary>
    public void OnGameStartClick()
    {
        // ゲームスタートボタンのアニメーションを再生
        m_GameStartButtonScript.Play_OnClick_AnimatorBlinking();
        // ボタンを消す
        m_TitleCanvasScript.TltleCanvasFade(true);
        // セレクト画面を表示
        m_TitleCanvasScript.SelectCanvasFade(true);
    }

    /// <summary>タイトルに戻る</summary>
    public void OnBackToTitle()
    {
        // セレクト画面を非表示
        m_TitleCanvasScript.SelectCanvasFade(false);
        // タイトルを表示
        m_TitleCanvasScript.TltleCanvasFade(false);
    }

    public void OnRoomCreationClick()
    {
        // ルーム作成のシーンに遷移する。
        Fade.Instance.FadeOut("RoomCreationScene");
    }
}
