using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class TitleController : MonoBehaviour
{
    // ゲームスタートボタン
    GameObject m_GameStartButton;
    // ゲームスタートボタンのスクリプトの参照
    GameStartButton m_GameStartButtonScript;

    void Start()
    {
        // ゲームスタートボタンを取得
        m_GameStartButton = GameObject.Find("GameStartButton");
        // ゲームスタートボタンのスクリプトの取得
        m_GameStartButtonScript = m_GameStartButton.GetComponent<GameStartButton>();
        // フェードイン
        Fade.Instance.FadeIn();
    }

    void Update()
    {
        // 現在の色を確認
        isSelectButtonColor();
    }

    void isSelectButtonColor()
    {
        // 現在のテキストの色を取得
        Color NormalColor = GameObject.Find("GameStartText").GetComponent<Text>().color;
        // フェードの色を取得
        Color FadeColor = Fade.Instance.Image.color;

        // 現在のテキストカラーの透明度が1以上で、現在セレクトされていない場合
        if (NormalColor.a >= 1 && 
            FadeColor.a <= 0 &&
            EventSystem.current.currentSelectedGameObject == null)
        {
            // ボタンをセットする
            EventSystem.current.SetSelectedGameObject(m_GameStartButton);
        }
    }

    /// <summary> ゲームスタートボタンが押された時</summary>
    public void OnGameStartClick()
    {
        // ゲームスタートボタンのアニメーションを再生
        m_GameStartButtonScript.Play_OnClick_AnimatorBlinking();
    }
}
