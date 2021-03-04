using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartButton : MonoBehaviour
{
    /// <summary>アニメーター</summary>
    Animator m_Animator;

    // アニメーション関係
    int s_OnClickTriggerHash = Animator.StringToHash("OnClickTrigger");

    void Start()
    {
        // アニメーターの取得
        m_Animator = GetComponent<Animator>();
    }

    public void Play_OnClick_AnimatorBlinking()
    {
        m_Animator.SetTrigger(s_OnClickTriggerHash);
    }
}
