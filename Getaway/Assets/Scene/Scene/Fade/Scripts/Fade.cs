using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fade : SingletonMOnoBehaviour<Fade>
{
    /// <summary>Imageの参照</summary>
    public Image m_FadeImage;

    /// <summary>フェードのフラグ</summary>
    bool m_FadeFlag = false;

    /// <summary>ロードされているか</summary>
    static bool m_Loaded = false;

    private void Awake()
    {
        if (m_Loaded) return;

        DontDestroyOnLoad(gameObject);
    }

    /// <summary>フェードイン</summary>
    public void FadeIn()
    {
        m_FadeImage.DOFade(0.0f, 1.0f).OnComplete(() => {
            m_FadeFlag = true;
        });
    }

    /// <summary>フェードアウト</summary>
    public void FadeOut()
    {
        m_FadeImage.DOFade(1.0f, 1.0f).OnComplete(() => {
            m_FadeFlag = false;
        });
    }

    /// <summary>フェードアウト(シーン遷移付き)</summary>
    /// <param name="SceneName">次のシーン名</param>
    public void FadeOut(string SceneName)
    {
        m_FadeImage.DOFade(1.0f, 1.0f).OnComplete(() => {
            // シーン遷移
            SceneManager.LoadScene(SceneName);
        });
    }

    /// <summary>色を返す(プロパティ)</summary>
    /// <returns></returns>
    public Image FadeImage
    {
        get { return m_FadeImage; }
        private set { m_FadeImage = value; }
    }

    /// <summary>フェードフラグを返す(プロパティ)</summary>
    /// <returns></returns>
    public bool FadeFrag
    {
        get { return m_FadeFlag; }
        private set { m_FadeFlag = value; }

    }
}
