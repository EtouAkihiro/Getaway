﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fade : SingletonMOnoBehaviour<Fade>
{
    /// <summary>フェードのフラグ</summary>
    bool m_FadeFlag = false;

    /// <summary>Imageの参照</summary>
    Image m_Image;

    void Start()
    {
        // Imageを取得
        m_Image = GetComponent<Image>();
    }

    /// <summary>フェードイン</summary>
    public void FadeIn()
    {
        m_Image.DOFade(0.0f, 1.0f).OnComplete(() => {
            m_FadeFlag = true;
        });
    }

    /// <summary>フェードアウト</summary>
    public void FadeOut()
    {
        m_Image.DOFade(1.0f, 1.0f).OnComplete(() => {
            m_FadeFlag = false;
        });
    }

    /// <summary>フェードアウト(シーン遷移付き)</summary>
    /// <param name="SceneName">次のシーン名</param>
    public void FadeOut(string SceneName)
    {
        m_Image.DOFade(1.0f, 1.0f).OnComplete(() => {
            // シーン遷移
            SceneManager.LoadScene(SceneName);
        });
    }

    /// <summary>色を返す(プロパティ)</summary>
    /// <returns></returns>
    public Image Image
    {
        get { return m_Image; }
        private set { m_Image = value; }
    }

    /// <summary>フェードフラグを返す(プロパティ)</summary>
    /// <returns></returns>
    public bool FadeFrag
    {
        get { return m_FadeFlag; }
        private set { m_FadeFlag = value; }

    }
}
