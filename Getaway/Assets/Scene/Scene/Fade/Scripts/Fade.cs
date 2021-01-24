using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

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
    void FadeIn() {
        m_Image.DOFade(0.0f, 1.0f);
    }

    /// <summary>フェードアウト</summary>
    void FadeOut() {
        m_Image.DOFade(1.0f, 1.0f);
    }
}
