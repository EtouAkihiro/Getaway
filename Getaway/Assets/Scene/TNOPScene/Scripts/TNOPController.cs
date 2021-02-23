using System.Collections;
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

    /// <summary>>一人でプレイする画面側の扉のスクリプトを参照</summary>
    Door m_SoloPlayDoorScript;
    /// <summary>複数人でプレイする画面側の扉のスクリプトを参照</summary>
    Door m_PluralPlayDoorScript;
    /// <summary>フェードのスクリプトの参照</summary>
    Fade m_FadeScript;
    /// <summary>メインカメラのスクリプトを参照</summary>
    MainCamera m_MainCameraScript;
    /// <summary>キャンバスのスクリプトを参照</summary>
    TNOPCanvas m_TNOPCanvasScript;

    /// ボタンのアニメーション
    /// <summary>クリックされた時に点滅を速くするアニメーション</summary>
    int s_OnClickSelectHash = Animator.StringToHash("OnClickTrigger");

    void Start()
    {
        // フェードオブジェクトの取得
        m_FadeScript = GameObject.FindGameObjectWithTag("Fade").GetComponent<Fade>();
        // キャンバスのスクリプトを取得
        m_TNOPCanvasScript = GameObject.Find("TNOPCanvas").GetComponent<TNOPCanvas>();
        // メインカメラのスクリプトの取得
        m_MainCameraScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MainCamera>();
        // 一人でプレイする画面のオブジェクトの長方形の位置を取得
        m_SoloSelectObjectsRT = GameObject.Find("soloSelectObjects").GetComponent<RectTransform>();
        // 複数人でプレイする画面のオブジェクトの長方形の位置を取得
        m_PluralPlaySelectObjectsRT = GameObject.Find("PluralPlaySelectObjects").GetComponent<RectTransform>();

        // 一人でプレイする画面側の扉のスクリプトを取得
        m_SoloPlayDoorScript = m_SoloPlayDoor.GetComponent<Door>();
        // 複数人でプレイする画面側の扉のスクリプトをを取得
        m_PluralPlayDoorScript = m_PluralPlayDoor.GetComponent<Door>();

        // フェードイン
        m_FadeScript.FadeIn();
        // 初期状態をアイドル
        m_State = State.Select;
    }

    void Update()
    {
        // 状態の更新
        Update_state();
    }

    /// <summary>状態の更新</summary>
    void Update_state()
    {
        // 状態ごとに更新
        switch (m_State)
        {
            case State.Select: Select(); break;
            case State.Solo: Solo(); break;
            case State.plural: Plural(); break;
        }
    }

    /// <summary>アイドル状態</summary>
    void Select()
    {

        // 入力値を入れる変数
        float H = 0.0f;

        // フェードインが終わった場合
        // 入力を受けれるようにする
        if (m_FadeScript.isFade())
        {
            // 左右の入力値
            H = Input.GetAxis("Horizontal");
        }

        // 閾値
        const float Threshold = 0.9f;

        // 入力値がゼロ以外だった場合
        if (H != 0)
        {
            // 左に入力された場合、状態を一人でプレイする状態にする
            if (H < -Threshold) m_State = State.Solo;
            // 右に入力された場合、状態を複数人でプレイする状態にする
            if (H > Threshold) m_State = State.plural;
        }

        // 状態がセレクト画面から変わった場合
        if (m_State != State.Select)
        {
            // 状態が、一人でプレイする状態だった場合
            if(m_State == State.Solo) {
                // 一人でプレイする画面を表示
                m_TNOPCanvasScript.SoloSelectDispPlayAnimator_Play(true);
                // 一人でプレイする状態のアニメーションを再生
                m_MainCameraScript.SoloSelectAnimator_Play(true);
            }
            // 状態が、複数人でプレイする状態だった場合
            else if (m_State == State.plural) {
                // 複数人でプレイする画面を表示
                m_TNOPCanvasScript.PluralPlaySelectDispPlayAnimatpr_Play(true);
                // 複数人でプレイする状態のアニメーションを再生
                m_MainCameraScript.PluralPlaySelectAnimator_Play(true);
            }
        }
    }

    /// <summary>一人プレイ</summary>
    void Solo()
    {
        // 選択されているオブジェクトがなかった場合
        if(EventSystem.current.currentSelectedGameObject == null && 
            m_SoloSelectObjectsRT.localPosition.x == 0) {
            // >一人でプレイする画面のプレイボタンをセット
            EventSystem.current.SetSelectedGameObject(m_SoloPlayButton);
        }

        // ドアが開き切った状態の場合
        // なおかつ、表示されているUIが消える状態の場合
        if (m_SoloPlayDoorScript.isOpeningAndClosingDoor() && 
            !m_TNOPCanvasScript.isSoloPlay_Display())
        {
            // ドアの奥に進むアニメーションを再生
            m_MainCameraScript.OnClick_SoloPlayAnimator_Play();
            // フェードアウトする
            m_FadeScript.FadeOut("SoloStageSelectScene");
        }
    }

    /// <summary>複数人でプレイ</summary>
    void Plural()
    {
        // 選択されているオブジェクトがなかった場合
        if (EventSystem.current.currentSelectedGameObject == null && 
            m_PluralPlaySelectObjectsRT.localPosition.x == 0)
        {
            // >複数人でプレイする画面のプレイボタンをセット
            EventSystem.current.SetSelectedGameObject(m_PluralPlayButton);
        }

        // ドアが開き切った状態の場合
        // なおかつ、表示されているUIが消える状態の場合
        if(m_PluralPlayDoorScript.isOpeningAndClosingDoor() &&
            !m_TNOPCanvasScript.isPluralPlay_Dosplay())
        {
            // ドアの奥に進むアニメーションを再生
            m_MainCameraScript.OnClick_PluralPlayAnimator_Play();
            // フェードアウトする
        }
    }

    /// <summary>ソロプレイでゲームを開始する</summary>
    public void OnClick_SoloPlay()
    {
        // 現在選択されているオブジェクトを取得
        GameObject SelectObject = EventSystem.current.currentSelectedGameObject;

        for(int i = 0; i < m_SoloSelectButtons.Length; i++)
        {
            if (m_SoloSelectButtons[i].gameObject == SelectObject)
            {
                // 決定されたボタンの点滅が速いアニメーションを再生
                m_SoloSelectButtons[i].GetComponent<Animator>().SetTrigger(s_OnClickSelectHash);
            }
            else
            {
                // 決定以外はボタンを選択出来ないようにする。
                m_SoloSelectButtons[i].interactable = false;
            }
        }

        // 一人でプレイする画面側のドアを開く
        m_SoloPlayDoorScript.OpenAndClosingDoorAnimator_Play(true);
        // 一人でプレイする画面を非表示にする
        m_MainCameraScript.OnClick_SoloPlayAnimator_Play();
    }

    /// <summary>複数人でゲームを開始する</summary>
    public void OnClick_PluralPlay()
    {
        // 現在選択されているオブジェクトを取得
        GameObject SelectObject = EventSystem.current.currentSelectedGameObject;

        for(int i = 0; i < m_PluralPlaySelectButtons.Length; i++)
        {
            if (m_PluralPlaySelectButtons[i].gameObject == SelectObject)
            {
                // 決定されたボタンの点滅が速いアニメーションを再生
                m_PluralPlaySelectButtons[i].GetComponent<Animator>().SetTrigger(s_OnClickSelectHash);
            }
            else
            {
                // 決定以外はボタンを選択出来ないようにする。
                m_PluralPlaySelectButtons[i].interactable = false;
            }
        }

        // 複数人でプレイする画面側のドアを開く
        m_PluralPlayDoorScript.OpenAndClosingDoorAnimator_Play(true);
        // 複数人でプレイする画面を非表示にする
        m_TNOPCanvasScript.OnClick_PluralPlayAnimator_Play();
    }

    /// <summary>一人でプレイするから選択画面に戻る</summary>
    public void OnClick_SoloBack()
    {
        // 現在の状態が、一人でプレイする状態だった場合
        if(m_State == State.Solo)
        {
            // 選択画面を表示
            m_TNOPCanvasScript.SoloSelectDispPlayAnimator_Play(false);
            // 選択画面へ移動するアニメーションを再生
            m_MainCameraScript.SoloSelectAnimator_Play(false);
            // 状態を選択状態に遷移
            m_State = State.Select;
            // 現在の選択されているオブジェクトがある場合
            if(EventSystem.current.currentSelectedGameObject != null)
            {
                // 選択されているオブジェクトを無しにする
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
    }


    /// <summary>複数人でプレイするから選択画面へ戻る</summary>
    public void OnClick_PluralBack()
    {
        // 現在の状態が、複数人でプレイする状態だった場合
        if(m_State == State.plural)
        {
            // 選択画面へ移動するアニメーションを再生
            m_MainCameraScript.PluralPlaySelectAnimator_Play(false);
            // 選択画面を表示
            m_TNOPCanvasScript.PluralPlaySelectDispPlayAnimatpr_Play(false);
            // 状態を選択状態に遷移
            m_State = State.Select;
            // 現在の選択されているオブジェクトがある場合
            if (EventSystem.current.currentSelectedGameObject != null)
            {
                // 選択されているオブジェクトを無しにする
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
    }
}
