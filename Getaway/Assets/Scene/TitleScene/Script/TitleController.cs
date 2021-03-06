using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class TitleController : MonoBehaviour
{

    enum State
    {
        Title,
        Select
    }

    /// <summary>ゲームスタートボタン</summary>
    GameObject m_GameStartButton;
    /// <summary>ゲームスタートボタンのスクリプトの参照</summary>
    GameStartButton m_GameStartButtonScript;

    /// <summary>タイトルキャンバスのスクリプトの参照</summary>
    TitleCanvas m_TitleCanvasScript;

    /// <summary>状態</summary>
    State m_State;

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
        // 初期状態をタイトル
        m_State = State.Title;
    }

    void Update()
    {
        switch (m_State)
        {
            case State.Title: Title(); break;
            case State.Select: Select(); break;
        }
    }

    /// <summary>タイトル</summary>
    void Title()
    {
        // 現在の色を確認
        isSelectButtonColor();
    }

    void Select() {}

    void isSelectButtonColor()
    {
        // 現在のボタンのノーマルの色を取得
        Color NormalColor = m_GameStartButton.GetComponent<Button>().colors.normalColor;
        // フェードの色を取得
        Color FadeColor = Fade.Instance.Image.color;

        if(NormalColor.a >= 1 &&
           FadeColor.a <= 0)
        {
            EventSystem.current.SetSelectedGameObject(m_GameStartButton);
        }
    }

    /// <summary> ゲームスタートボタンが押された時</summary>
    public void OnGameStartClick()
    {
        // ゲームスタートボタンのアニメーションを再生
        m_GameStartButtonScript.Play_OnClick_AnimatorBlinking();
        // ボタンを消す
        m_TitleCanvasScript.TltleCanvasFade(true);
        // 状態をセレクトに変更
        m_State = State.Select;
    }
}
