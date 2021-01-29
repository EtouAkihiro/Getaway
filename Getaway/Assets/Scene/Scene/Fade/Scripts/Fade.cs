using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    // imageの参照
    Image m_Image;

    void Start() {
        // Imageを取得
        m_Image = GetComponent<Image>();
    }

    void Update() {
    }

    /// <summary>フェードイン</summary>
    public void FadeIn() {
        m_Image.DOFade(0.0f, 1.0f);
    }

    /// <summary>フェードアウト</summary>
    public void FadeOut() {
        m_Image.DOFade(1.0f, 1.0f);
    }

    /// <summary>フェードアウト(シーン遷移付き)</summary>
    /// <param name="SceneName">次のシーン名</param>
    public void FadeOut(string SceneName) {
        GetComponent<Image>().DOFade(1.0f, 1.0f).OnComplete(() => {
            // シーン遷移
            SceneManager.LoadScene(SceneName);
        });
    }
}
