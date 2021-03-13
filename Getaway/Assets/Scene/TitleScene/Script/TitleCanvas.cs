using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class TitleCanvas : MonoBehaviour
{
    /// <summary>タイトルオブジェクト</summary>
    public GameObject m_Title;
    /// <summary>セレクトオブジェクト</summary>
    public GameObject m_Select;
    /// <summary>ゲームスタート</summary>
    public GameObject m_GameStart;
    /// <summary>名前を入力</summary>
    public GameObject m_NameInput;

    public GameObject m_Test;

    /// <summary>アニメーター</summary>
    Animator m_Animator;

    // アニメーションハッシュ
    int s_TitleDisPlayFrag = Animator.StringToHash("TiteDisPlayFrag");
    int s_SelectDisPlayFrag = Animator.StringToHash("SelectDisPlayFrag");

    void Start()
    {
        // アニメーターの取得
        m_Animator = GetComponent<Animator>();
        // セレクトオブジェクトを非表示にする
        m_Select.SetActive(false);

    }

    /// <summary>タイトルのフェードをする(true:フェードアウト, false:フェードイン)</summary>
    /// <param name="Frag">フラグ</param>
    public void TltleCanvasFade(bool Frag)
    {
        m_Animator.SetBool(s_TitleDisPlayFrag, Frag);
    }

    /// <summary>セレクトのフェードする(true:フェードアウト, false:フェードイン)</summary>
    /// <param name="Frag">フラグ</param>
    public void SelectCanvasFade(bool Frag)
    {
        m_Animator.SetBool(s_SelectDisPlayFrag, Frag);
    }

    /// <summary>タイトルのボタンセット</summary>
    public void ButtonSet(int Frag)
    {
        // フラグごとにボタンをセットする。
        switch (Frag)
        {
            case 0: EventSystem.current.SetSelectedGameObject(m_GameStart); break;
            case 1: EventSystem.current.SetSelectedGameObject(m_Test); break;
        }
    }

    /// <summary>UIの表示・非表示</summary>
    /// <param name="Frag"></param>
    public void Active_UI(int Frag)
    {
        // フラグが0だった場合、
        // タイトルを非表示にして
        // セレクトを表示する。
        if(Frag == 0)
        {
            m_Select.SetActive(true);
            m_Title.SetActive(false);
        }
        // フラグが1だった場合
        // セレクトを非表示にして
        // タイトルを表示する。
        else if(Frag == 1)
        {
            m_Title.SetActive(true);
            m_Select.SetActive(false);
        }
    }
}
