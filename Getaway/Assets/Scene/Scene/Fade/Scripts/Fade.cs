using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class Fade : SingletonMOnoBehaviour<Fade>
{
    /// <summary>Imageの参照</summary>
    public Image m_FadeImage;

    /// <summary>キャンバス</summary>
    Canvas m_Canvas;

    /// <summary>最大描画優先度</summary>
    int m_MaxSortOrder = 100;
    /// <summary>最小描画優先度</summary>
    int m_MinSortOrder = -1;
    /// <summary>フェードのフラグ</summary>
    bool m_FadeFlag = false;

    /// <summary>ロードされているか</summary>
    static bool m_Loaded = false;

    void Awake()
    {
        // ロードされた状態なら処理を行わない。
        if (m_Loaded) return;
        // ロードされた状態にする。
        m_Loaded = true;
        // シーン切り替えで破棄されない。
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // キャンバスを取得
        m_Canvas = GetComponent<Canvas>();
    }

    /// <summary>フェードイン</summary>
    public void FadeIn()
    {
        m_FadeImage.DOFade(0.0f, 1.0f).OnComplete(() => {
            m_FadeFlag = true;
            m_Canvas.sortingOrder = m_MinSortOrder;
        });
    }

    /// <summary>フェードアウト</summary>
    public void FadeOut()
    {
        // 現在の描画優先度が最小描画優先度だった場合
        // 描画優先度を最大描画優先度にする。
        if(m_Canvas.sortingOrder == m_MinSortOrder)
        {
            m_Canvas.sortingOrder = m_MaxSortOrder;
        }

        m_FadeImage.DOFade(1.0f, 1.0f).OnComplete(() => {
            m_FadeFlag = false;
        });
    }

    /// <summary>フェードアウト(シーン遷移付き)</summary>
    /// <param name="SceneName">次のシーン名</param>
    public void FadeOut(string SceneName)
    {
        // 現在の描画優先度が最小描画優先度だった場合
        // 描画優先度を最大描画優先度にする。
        if (m_Canvas.sortingOrder == m_MinSortOrder)
        {
            m_Canvas.sortingOrder = m_MaxSortOrder;
        }

        m_FadeImage.DOFade(1.0f, 1.0f).OnComplete(() => {
            // シーン遷移
            SceneManager.LoadScene(SceneName);
        });
    }

    /// <summary>フェードアウト(Photonシーン遷移付き)</summary>
    /// <param name="SceneLaval">シーンレベル</param>
    public void FadeOut(int SceneLaval)
    {
        // 現在の描画優先度が最小描画優先度だった場合
        // 描画優先度を最大描画優先度にする。
        if (m_Canvas.sortingOrder == m_MinSortOrder)
        {
            m_Canvas.sortingOrder = m_MaxSortOrder;
        }

        m_FadeImage.DOFade(1.0f, 1.0f).OnComplete(() => {
            // シーン遷移
            PhotonNetwork.LoadLevel(SceneLaval);
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
