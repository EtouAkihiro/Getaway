using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TitleCanvas : MonoBehaviour
{
    /// <summary>アニメーター</summary>
    Animator m_Animator;

    int s_TitleCanvasFade = Animator.StringToHash("TitleFadeFrag");

    void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    /// <summary>タイトルのフェードをする(true:フェードアウト, false:フェードイン)</summary>
    /// <param name="Frag">フラグ</param>
    public void TltleCanvasFade(bool Frag)
    {
        m_Animator.SetBool(s_TitleCanvasFade, Frag);
    }
}
