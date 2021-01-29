using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class TitleController : MonoBehaviour
{
    // Fadeオブジェクト
    GameObject m_Fade;
    // タイトルキャンバス
    GameObject m_TitleCanvas;
    // ゲームスタートボタン
    GameObject m_GameStartButton;

    // アニメーション関係
    int s_TitleHash = Animator.StringToHash("TitleTrigger");

    void Start() {
        // Fadeオブジェクトを取得
        m_Fade = GameObject.FindGameObjectWithTag("Fade");
        // タイトルキャンバスを取得
        m_TitleCanvas = GameObject.Find("TitleCanvas");
        // ゲームスタートボタンを取得
        m_GameStartButton = GameObject.Find("GameStartButton");
        // フェードイン
        FadeIn();
    }

    void Update() {
        // 現在の色を確認
        isSelectButtonColor();
    }

    /// <summary>フェードイン</summary>
    void FadeIn() {
        m_Fade.GetComponent<Image>().DOFade(0.0f, 1.0f);
    }

    /// <summary>フェードアウト(シーン遷移付き)</summary>
    void FadeOut() {
        m_Fade.GetComponent<Image>().DOFade(1.0f, 1.0f).OnComplete(() => {
            // シーン遷移
            SceneManager.LoadScene("TNOPScene");
        });
    }

    void isSelectButtonColor() {
        // 現在のテキストの色を取得
        Color NormalColor = GameObject.Find("GameStartText").GetComponent<Text>().color;
        // フェードの色を取得
        Color FadeColor = m_Fade.GetComponent<Image>().color;

        // 現在のテキストカラーの透明度が1以上で、現在セレクトされていない場合
        if (NormalColor.a >= 1 && 
            FadeColor.a <= 0 &&
            EventSystem.current.currentSelectedGameObject == null) {
            // ボタンをセットする
            EventSystem.current.SetSelectedGameObject(m_GameStartButton);
        }
    }

    /// <summary> ゲームスタートボタンが押された時</summary>
    public void OnGameStartClick() {
        GameObject.Find("GameStartButton").GetComponent<Animator>().SetTrigger(s_TitleHash);
        FadeOut();
    }
}
