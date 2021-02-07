using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNOPCanvas : MonoBehaviour
{
    /// <summary>一人でプレイする画面の表示・非表示のフラグ</summary>
    bool m_SoloPlay_DisplayFlag = false;
    /// <summary>複数人でプレイする画面の表示・非表示のフラグ</summary>
    bool m_PluralPlay_DosplayFlag = false;

    /// <summary>アニメーターを参照</summary>
    Animator m_Animator;

    /// アニメーション
    /// <summary>一人でプレイする画面を表示するアニメーション</summary>
    int s_SoloSelectDispPlayHash = Animator.StringToHash("soloSelectFrag");
    /// <summary>複数でプレイする画面を表示するアニメーション</summary>
    int s_PluralPlaySelectDispPlayHash = Animator.StringToHash("PluralPlaySelectFrag");
    /// <summary>>一人でプレイする画面を非表示にするアニメーション</summary>
    int s_OnClick_SoloPlayHash = Animator.StringToHash("OnClick_SoloPlayTrigger");
    /// <summary>>複数でプレイする画面を非表示にするアニメーション</summary>
    int s_OnClick_PluralPlayHash = Animator.StringToHash("OnClick_PluralPlayTrigger ");

    /// <summary>開始</summary>
    void Start() {
        // アニメーターを参照
        m_Animator = GetComponent<Animator>();
    }

    /// <summary>一人でプレイする画面が表示されている</summary>
    public void SoloPlay_DisPlay() {
        // 表示されているフラグにする
        m_SoloPlay_DisplayFlag = true;
    }

    /// <summary>一人でプレイする画面が非表示されている</summary>
    public void SoloPlay_Hide() {
        // 非表示になっているフラグにする
        m_SoloPlay_DisplayFlag = false;
    }

    /// <summary>一人でプレイする画面の表示・非表示のフラグを返す</summary>
    /// <returns></returns>
    public bool isSoloPlay_Display() {
        return m_SoloPlay_DisplayFlag;
    }

    /// <summary>複数人でプレイする画面が表示されている</summary>
    public void PluralPlay_DisPlay() {
        // 表示されているフラグにする
        m_PluralPlay_DosplayFlag = true;
    }

    /// <summary>複数人でプレイする画面が非表示されている</summary>
    public void PluralPlay_Hide() {
        // 非表示になっているフラグにする
        m_PluralPlay_DosplayFlag = false;
    }

    /// <summary>複数人でプレイする画面の表示・非表示のフラグを返す</summary>
    /// <returns></returns>
    public bool isPluralPlay_Dosplay() {
        return m_PluralPlay_DosplayFlag;
    }
}
