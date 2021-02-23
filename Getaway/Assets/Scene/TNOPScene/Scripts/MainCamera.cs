using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    /// <summary>アニメーター</summary>
    Animator m_Animator;

    /// <summary>ドアの開閉のフラグ</summary>
    bool m_DoorOpenFrag = false;

    /// カメラのアニメーション
    /// <summary>一人でプレイする画面に移動するアニメーション</summary>
    int s_SoloSelectHash = Animator.StringToHash("SoloSelectFrag");
    /// <summary>複数人でプレイする画面に移動するアニメーション</summary>
    int s_PluralPlaySelectHash = Animator.StringToHash("PluralPlaySelectFrag");
    /// <summary>>一人でプレイする側の扉の奥に進むアニメーション</summary>
    int s_OnClick_SoloPlayHash = Animator.StringToHash("OnClick_SoloPlayTrigger");
    /// <summary>複数人でプレイする側の扉の奥に進むアニメーション</summary>
    int s_OnClick_PluralPlayHash = Animator.StringToHash("OnClick_PluralPlayTrigger");

    void Start()
    {
        // アニメーターの参照
        m_Animator = GetComponent<Animator>();
    }

    /// <summary>一人でプレイする画面に移動するアニメーションを再生</summary>
    /// <param name="Frag">フラグ</param>
    public void SoloSelectAnimator_Play(bool Frag)
    {
        m_Animator.SetBool(s_SoloSelectHash, Frag);
    }

    /// <summary>複数人でプレイする画面に移動するアニメーションを再生</summary>
    /// <param name="Frag">フラグ</param>
    public void PluralPlaySelectAnimator_Play(bool Frag)
    {
        m_Animator.SetBool(s_PluralPlaySelectHash, Frag);
    }

    ///<summary>一人でプレイする側の扉の奥に進むアニメーションを再生</summary>
    public void OnClick_SoloPlayAnimator_Play()
    {
        m_Animator.SetTrigger(s_OnClick_SoloPlayHash);
    }

    ///<summary>複数人でプレイする側の扉の奥に進むアニメーションを再生</summary>
    public void OnClick_PluralPlayAnimator_Play()
    {
        m_Animator.SetTrigger(s_OnClick_PluralPlayHash);
    }

    void DoorState()
    {
        // ドアが閉じている場合
        if (!m_DoorOpenFrag)
        {
            // ドアを開いている状態にする
            m_DoorOpenFrag = true;
        }
        // ドアが開いている場合
        else
        {
            // ドアを閉じた状態にする
            m_DoorOpenFrag = false;
        }
    }

    /// <summary>ドアのフラグを返す</summary>
    /// <returns></returns>
    bool isDoorOpenFrag()
    {
        return m_DoorOpenFrag;
    }
}
