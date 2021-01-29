using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class TitleController : MonoBehaviour
{
    // タイトルキャンバス
    GameObject m_TitleCanvas;
    // ゲームスタートボタン
    GameObject m_GameStartButton;

    // Fadeスクリプトの参照
    Fade m_Fade;

    // アニメーション関係
    int s_TitleHash = Animator.StringToHash("TitleTrigger");

    void Start() {
        // Fadeオブジェクトを取得
        m_Fade = GameObject.FindGameObjectWithTag("Fade").GetComponent<Fade>();
        // タイトルキャンバスを取得
        m_TitleCanvas = GameObject.Find("TitleCanvas");
        // ゲームスタートボタンを取得
        m_GameStartButton = GameObject.Find("GameStartButton");
        // フェードイン
        m_Fade.FadeIn();
    }

    void Update() {
        // 現在の色を確認
        isSelectButtonColor();
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
        // ゲームスタートボタンのアニメーションを再生
        m_GameStartButton.GetComponent<Animator>().SetTrigger(s_TitleHash);
        // Fadeアウトしたら次のシーンへ
        m_Fade.FadeOut("TNOPScene");
    }
}
