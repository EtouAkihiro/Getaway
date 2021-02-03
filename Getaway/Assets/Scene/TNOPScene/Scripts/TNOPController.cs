﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class TNOPController : MonoBehaviour
{
    /// <summary>一人でプレイする画面の初期設定のプレイボタン</summary>
    public GameObject m_SoloPlayButton;
    /// <summary>複数人でプレイする画面の初期設定のプレイボタン</summary>
    public GameObject m_PluralPlayButton;
    /// <summary>一人でプレイする画面側の扉</summary>
    public GameObject m_SoloPlayDoor;
    /// <summary>複数人でプレイする画面側の扉</summary>
    public GameObject m_PluralPlayDoor;
    /// <summary>一人でプレイする画面のボタン</summary>
    public Button[] m_SoloSelectButtons;
    /// <summary>複数人でプレイする画面のボタン</summary>
    public Button[] m_PluralPlaySelectButtons;

    enum State
    {
        Select,
        Solo,
        plural
    }

    /// <summary>一人でプレイする画面のオブジェクトの長方形の位置を参照</summary>
    RectTransform m_SoloSelectObjectsRT;
    /// <summary>複数人でプレイする画面のオブジェクトの長方形の位置を参照</summary>
    RectTransform m_PluralPlaySelectObjectsRT;

    /// <summary>状態</summary>
    State m_State;
    /// <summary>メインカメラアニメーター</summary>
    Animator m_MainCameraAnimator;
    /// <summary>キャンバスアニメーター</summary>
    Animator m_CanvasAnimator;
    /// <summary>>一人でプレイする画面側の扉のスクリプトを参照</summary>
    Door m_SoloPlayDoorScript;
    /// <summary>複数人でプレイする画面側の扉のスクリプトを参照</summary>
    Door m_PluralPlayDoorScript;
    /// <summary>フェードの参照</summary>
    Fade m_Fade;

    /// カメラのアニメーション
    /// <summary>一人でプレイする画面に移動するアニメーション</summary>
    int s_SoloSelectHash = Animator.StringToHash("SoloSelectFrag");
    /// <summary>複数人でプレイする画面に移動するアニメーション</summary>
    int s_PluralPlaySelectHash = Animator.StringToHash("PluralPlaySelectFrag");
    /// <summary>>一人でプレイする側の扉の奥に進むアニメーション</summary>
    int s_MainCamera_OnClick_SoloPlayHash = Animator.StringToHash("OnClick_SoloPlayTrigger");
    /// <summary>複数人でプレイする側の扉の奥に進むアニメーション</summary>
    int s_MainCamera_OnClick_PluralPlayHash = Animator.StringToHash("OnClick_PluralPlayTrigger");


    /// キャンバスのアニメーション
    /// <summary>一人でプレイする画面を表示するアニメーション</summary>
    int s_SoloSelectDispPlayHash = Animator.StringToHash("soloSelectFrag");
    /// <summary>複数でプレイする画面を表示するアニメーション</summary>
    int s_PluralPlaySelectDispPlayHash = Animator.StringToHash("PluralPlaySelectFrag");
    /// <summary>扉の開閉のフラグ</summary>
    int s_OpeningAndClosingDoorHash = Animator.StringToHash("DoorFlag");

    /// ボタンのアニメーション
    /// <summary>クリックされた時に点滅を速くするアニメーション</summary>
    int s_OnClickSelectHash = Animator.StringToHash("OnClickTrigger");

    void Start() {
        // フェードオブジェクトの取得
        // m_Fade = GameObject.FindGameObjectWithTag("Fade").GetComponent<Fade>();
        // メインカメラのアニメーターの取得
        m_MainCameraAnimator = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
        // キャンバスのアニメーターの取得
        m_CanvasAnimator = GameObject.Find("TNOPCanvas").GetComponent<Animator>();
        // 一人でプレイする画面のオブジェクトの長方形の位置を取得
        m_SoloSelectObjectsRT = GameObject.Find("soloSelectObjects").GetComponent<RectTransform>();
        // 複数人でプレイする画面のオブジェクトの長方形の位置を取得
        m_PluralPlaySelectObjectsRT = GameObject.Find("PluralPlaySelectObjects").GetComponent<RectTransform>();

        // 一人でプレイする画面側の扉のスクリプトを取得
        m_SoloPlayDoorScript = m_SoloPlayDoor.GetComponent<Door>();
        // 複数人でプレイする画面側の扉のスクリプトをを取得
        m_PluralPlayDoorScript = m_PluralPlayDoor.GetComponent<Door>();

        // フェードイン
        // m_Fade.FadeIn();
        // 初期状態をアイドル
        m_State = State.Select;
    }

    void Update() {
        // 状態の更新
        Update_state();
    }

    /// <summary>状態の更新</summary>
    void Update_state() {
        // 状態ごとに更新
        switch (m_State)
        {
            case State.Select: Select(); break;
            case State.Solo: Solo(); break;
            case State.plural: Plural(); break;
        }
    }

    /// <summary>アイドル状態</summary>
    void Select() {
        // 左右の入力値
        float H = Input.GetAxis("Horizontal");

        // 閾値
        const float Threshold = 0.9f;

        // 入力値がゼロ以外だった場合
        if (H != 0) {
            // 左に入力された場合、状態を一人でプレイする状態にする
            if (H < -Threshold) m_State = State.Solo;
            // 右に入力された場合、状態を複数人でプレイする状態にする
            if (H > Threshold) m_State = State.plural;
        }

        // 状態がセレクト画面から変わった場合
        if (m_State != State.Select) {
            // 状態が、一人でプレイする状態だった場合
            if(m_State == State.Solo) {
                // 一人でプレイする画面を表示
                m_CanvasAnimator.SetBool(s_SoloSelectDispPlayHash, true);
                // 一人でプレイする状態のアニメーションを再生
                m_MainCameraAnimator.SetBool(s_SoloSelectHash, true);
            }
            // 状態が、複数人でプレイする状態だった場合
            else if (m_State == State.plural) {
                // 複数人でプレイする画面を表示
                m_CanvasAnimator.SetBool(s_PluralPlaySelectDispPlayHash, true);
                // 複数人でプレイする状態のアニメーションを再生
                m_MainCameraAnimator.SetBool(s_PluralPlaySelectHash, true);
            }
        }
    }

    /// <summary>一人プレイ</summary>
    void Solo() {
        // 選択されているオブジェクトがなかった場合
        if(EventSystem.current.currentSelectedGameObject == null && m_SoloSelectObjectsRT.localPosition.x == 0) {
            // >一人でプレイする画面のプレイボタンをセット
            EventSystem.current.SetSelectedGameObject(m_SoloPlayButton);
        }

        // ドアが開き切った状態の場合
        if (m_SoloPlayDoorScript.isOpeningAndClosingDoor()) {
            // ドアの奥に進むアニメーションを再生
            m_MainCameraAnimator.SetTrigger(s_MainCamera_OnClick_SoloPlayHash);
        }
    }

    /// <summary>複数人でプレイ</summary>
    void Plural() {
        // 選択されているオブジェクトがなかった場合
        if (EventSystem.current.currentSelectedGameObject == null && m_PluralPlaySelectObjectsRT.localPosition.x == 0) {
            // >複数人でプレイする画面のプレイボタンをセット
            EventSystem.current.SetSelectedGameObject(m_PluralPlayButton);
        }
    }

    /// <summary>ソロプレイでゲームを開始する</summary>
    public void OnClick_SoloPlay() {
        // 現在選択されているオブジェクトを取得
        GameObject SelectObject = EventSystem.current.currentSelectedGameObject;

        for(int i = 0; i < m_SoloSelectButtons.Length; i++) {
            if (m_SoloSelectButtons[i].gameObject == SelectObject) {
                // 決定されたボタンの点滅が速いアニメーションを再生
                m_SoloSelectButtons[i].GetComponent<Animator>().SetTrigger(s_OnClickSelectHash);
            } else {
                // 決定以外はボタンを選択出来ないようにする。
                m_SoloSelectButtons[i].interactable = false;
            }
        }

        // 一人でプレイする画面側のドアを開く
        m_SoloPlayDoor.GetComponent<Animator>().SetBool(s_OpeningAndClosingDoorHash, true);
    }

    /// <summary>複数人でゲームを開始する</summary>
    public void OnClick_PluralPlay() {
    }

    /// <summary>一人でプレイするから選択画面に戻る</summary>
    public void OnClick_SoloBack() {
        // 現在の状態が、一人でプレイする状態だった場合
        if(m_State == State.Solo) {
            // 選択画面へ移動するアニメーションを再生
            m_MainCameraAnimator.SetBool(s_SoloSelectHash, false);
            // 選択画面を表示
            m_CanvasAnimator.SetBool(s_SoloSelectDispPlayHash, false);
            // 状態を選択状態に遷移
            m_State = State.Select;
            // 現在の選択されているオブジェクトがある場合
            if(EventSystem.current.currentSelectedGameObject != null) {
                // 選択されているオブジェクトを無しにする
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
    }


    /// <summary>複数人でプレイするから選択画面へ戻る</summary>
    public void OnClick_PluralBack() {
        // 現在の状態が、複数人でプレイする状態だった場合
        if(m_State == State.plural) {
            // 選択画面へ移動するアニメーションを再生
            m_MainCameraAnimator.SetBool(s_PluralPlaySelectHash, false);
            // 選択画面を表示
            m_CanvasAnimator.SetBool(s_PluralPlaySelectDispPlayHash, false);
            // 状態を選択状態に遷移
            m_State = State.Select;
            // 現在の選択されているオブジェクトがある場合
            if (EventSystem.current.currentSelectedGameObject != null) {
                // 選択されているオブジェクトを無しにする
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
    }
}
