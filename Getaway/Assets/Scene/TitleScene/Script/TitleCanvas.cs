using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TitleCanvas : MonoBehaviour
{
    /// <summary>タイトル</summary>
    public GameObject m_Title;

    /// <summary>アニメーター</summary>
    Animator m_Animator;

    int s_TitleDisPlayFrag = Animator.StringToHash("TiteDisPlayFrag");

    void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    /// <summary>タイトルのフェードをする(true:フェードアウト, false:フェードイン)</summary>
    /// <param name="Frag">フラグ</param>
    public void TltleCanvasFade(bool Frag)
    {
        m_Animator.SetBool(s_TitleDisPlayFrag, Frag);
    }
}
