using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    // Fadeオブジェクト
    GameObject m_Fade;
    // タイトルキャンバス
    GameObject m_TitleCanvas;

    // アニメーション関係
    int s_TitleHash = Animator.StringToHash("TitleTrigger");

    void Start() {
        // Fadeオブジェクトを取得
        m_Fade = GameObject.FindGameObjectWithTag("Fade");
        // タイトルキャンバスを取得
        m_TitleCanvas = GameObject.Find("TitleCanvas");
        // フェードイン
        FadeIn();
    }

    void Update() {   
    }

    /// <summary>フェードイン</summary>
    void FadeIn() {
        m_Fade.GetComponent<Image>().DOFade(0.0f, 1.0f);
    }

    /// <summary>フェードアウト(シーン遷移付き)</summary>
    void FadeOut() {
        m_Fade.GetComponent<Image>().DOFade(1.0f, 1.0f).OnComplete(() => {
            // シーン遷移
            // SceneManager.LoadScene("");
        });
    }
}
