using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class TitleController : MonoBehaviour
{
    /// <summary>エラーメッセージのオブジェクト</summary>
    public GameObject m_WarningTextObject;

    /// <summary>名前の入力</summary>
    public InputField m_NameInputField;
    /// <summary>パスワードの入力</summary>
    public InputField m_PaswadInputField;

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

    /// <summary>ルーム作成シーンに遷移する。</summary>
    public void OnRoomCreationClick()
    {
        // 入力された名前を取得
        string Name = m_NameInputField.text;

        // もし、名前が入力されていなかったら、
        // エラーメッセージを表示する。
        if (Name == "")
        {
            WarningTextDisPlay();
            return;
        }

        // ルーム作成のシーンに遷移する。
        Fade.Instance.FadeOut("RoomCreationScene");
    }

    /// <summary>エラーメッセージを表示・非表示</summary>
    void WarningTextDisPlay()
    {
        // テキストオブジェクトが非表示になってたら、表示する。
        if (m_WarningTextObject.activeSelf == false) m_WarningTextObject.SetActive(true);

        // エラーメッセージのテキストを取得
        Text WarningText = m_WarningTextObject.GetComponent<Text>();

        // 現在のテキストカラーを取得
        Color WarningTextColor = WarningText.color;
        // Alpha値に1を入れる。
        WarningTextColor.a = 1.0f;
        // テキストカラーを反映
        WarningText.color = WarningTextColor;

        // エラーメッセージをフェードインする。
        WarningText.DOFade(0.0f, 1.0f).OnComplete(() => {
            m_WarningTextObject.SetActive(false);
        });
    }
}
