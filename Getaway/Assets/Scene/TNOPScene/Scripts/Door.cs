using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    /// <summary>開閉のフラグ</summary>
    /// trueが開いている状態・falseが閉じている状態
    bool m_OpeningAndClosingDoorFrag = false;

    /// <summary>アニメーター</summary>
    Animator m_Animator;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    /// ドアのアニメーション
    /// <summary>扉の開閉のフラグ</summary>
    int s_OpeningAndClosingDoorHash = Animator.StringToHash("DoorFlag");

    public void OpenAndClosingDoorAnimator_Play(bool Frag)
    {
        m_Animator.SetBool(s_OpeningAndClosingDoorHash, Frag);
    }

    /// <summary>ドアが開き終わった時</summary>
    public void OpenDoor()
    {
        m_OpeningAndClosingDoorFrag = true;
    }

    /// <summary>ドアが閉まり切った時</summary>
    public void CloseDoor()
    {
        m_OpeningAndClosingDoorFrag = false;
    }

    /// <summary>開閉のフラグを返す</summary>
    /// <returns>trueが開いている状態・falseが閉じている状態</returns>
    public bool isOpeningAndClosingDoor()
    {
        return m_OpeningAndClosingDoorFrag;
    }
}
