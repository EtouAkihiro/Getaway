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
    /// <summary>Photonビュー</summary>
    PhotonView m_PhotonView;

    /// <summary>最大描画優先度</summary>
    int m_MaxSortOrder = 100;
    /// <summary>最小描画優先度</summary>
    int m_MinSortOrder = -1;
    /// <summary>フェードのフラグ</summary>
    bool m_FadeFlag = false;

    void Awake()
    {
        // 現在あるFadeタグを持っているオブジェクトを取得
        GameObject[] Fades = GameObject.FindGameObjectsWithTag("Fade");
        // Fadeタグを持っているオブジェクトが2個以上ある場合、
        // 新しく生成されたオブジェクトを削除する。
        if(Fades.Length >= 2)
        {
            for(int i = 0; i < Fades.Length; i++)
            {
                if(i != 0)
                {
                    Destroy(Fades[i]);
                }
            }
        }
        // ２個未満だった場合、
        // シーン遷移で破棄されないオブジェクトにする。
        else
        {
            // シーン切り替えで破棄されない。
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        // キャンバスを取得
        m_Canvas = GetComponent<Canvas>();
        // Photonビューの取得
        m_PhotonView = GetComponent<PhotonView>();
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
            m_FadeFlag = false;
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
