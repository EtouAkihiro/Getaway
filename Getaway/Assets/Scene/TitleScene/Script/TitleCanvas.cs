using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

/// <summary>タイトルキャンバスの管理</summary>
public class TitleCanvas : MonoBehaviour
{
    /// <summary>プレイヤー名が入力されていないというエラーメッセージのオブジェクト</summary>
    public GameObject m_NoPlayerNameErrorMessageObject;
    /// <summary>ルームがないというエラーメッセージのオブジェクト</summary>
    public GameObject m_NoRoomErrorMessageObject;
    /// <summary>タイトルオブジェクト</summary>
    public GameObject m_Title;
    /// <summary>セレクトオブジェクト</summary>
    public GameObject m_Select;
    /// <summary>ゲームスタート</summary>
    public GameObject m_GameStart;
    /// <summary>ゲームスタートボタン</summary>
    public GameObject m_GameStartButton;
    /// <summary>ルーム名を入力する画面</summary>
    public GameObject m_RoomNameInput;
    /// <summary>ルーム作成画面</summary>
    public GameObject m_RoomCreationInput;

    /// <summary>名前の入力</summary>
    public InputField m_NameInputField;
    /// <summary>ルーム名の入力</summary>
    public InputField m_RoomInputField;
    /// <summary>ルーム名(ルーム参加用)の入力</summary>
    public InputField m_RoomNameInputField;

    /// <summary>ゲームスタートボタンのスクリプトの参照</summary>
    GameStartButton m_GameStartButtonScript;

    /// <summary>アニメーター</summary>
    Animator m_Animator;

    /// <summary>ゲームスタートボタンが押された状態のフラグ</summary>
    bool m_GameStartButtonClickFrag = false;

    // アニメーションハッシュ
    int s_TitleDisPlayFrag = Animator.StringToHash("TiteDisPlayFrag");
    int s_SelectDisPlayFrag = Animator.StringToHash("SelectDisPlayFrag");

    void Start()
    {
        // ゲームスタートボタンのスクリプトの取得
        m_GameStartButtonScript = m_GameStartButton.GetComponent<GameStartButton>();
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
            case 1: EventSystem.current.SetSelectedGameObject(null); break;
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

    /// <summary> ゲームスタートボタンが押された時</summary>
    public void OnGameStartClick()
    {
        // ゲームスタートボタンのアニメーションを再生
        m_GameStartButtonScript.Play_OnClick_AnimatorBlinking();
        // ボタンを消す
        TltleCanvasFade(true);
        // セレクト画面を表示
        SelectCanvasFade(true);
        // サーバーに接続
        PhotonManager.Instance.ConnectedToServer();
        // ロビーに接続
        PhotonManager.Instance.JointLobby();
        // 押された状態にする。
        m_GameStartButtonClickFrag = true;
    }

    /// <summary>タイトルに戻る</summary>
    public void OnBackToTitle()
    {
        // セレクト画面を非表示
        SelectCanvasFade(false);
        // タイトルを表示
        TltleCanvasFade(false);
        // ロビーから切断
        PhotonManager.Instance.LeaveLobby();
        // サーバーから切断
        PhotonManager.Instance.DisconnectSavar();
        // 押されていない状態にする。
        m_GameStartButtonClickFrag = false;
    }

    /// <summary>プレイヤーの名前を登録</summary>
    public void OnSetPlayerName()
    {
        // プレイヤーの名前を取得
        string PlayerName = m_NameInputField.text;

        // もし、名前が入力された場合
        if (PlayerName != "")
        {
            // プレイヤー名を登録
            PhotonManager.Instance.SetPlayerName(PlayerName);
        }
        else
        {
            print("プレイヤー名が入力されてません。");
        }
    }

    /// <summary>ルーム作成画面の表示・非表示する。</summary>
    public void OnRoomCreationClick()
    {
        // 入力された名前を取得
        string Name = m_NameInputField.text;

        // もし、名前が入力されていなかったら、
        // エラーメッセージを表示する。
        if (Name == "")
        {
            NoPlayerNameErrorMessageTextDisPlay();
            return;
        }

        // ルーム作成が非表示だった場合、表示する。
        // ルーム作成が表示だった場合、非表示する。
        if (!m_RoomCreationInput.activeSelf)
        {
            m_RoomCreationInput.SetActive(true);
            m_Select.SetActive(false);
        }
        else
        {
            m_RoomCreationInput.SetActive(false);
            m_Select.SetActive(true);
        }
    }

    /// <summary>ルーム画面にシーン遷移</summary>
    public void OnRoomClick()
    {
        // ルーム名を取得
        string RoomName = m_RoomInputField.text;
        // ルーム名が入力されていなかったら、シーン遷移しない
        if(RoomName == "") return;

        // ルームを作成
        PhotonManager.Instance.CreateRoom(RoomName);
        // ルームシーンに遷移
        Fade.Instance.FadeOut("RoomScene");
    }

    /// <summary>パスワードの入力画面を表示・非表示</summary>
    public void OnRoomNameClick()
    {
        // 入力された名前を取得
        string Name = m_NameInputField.text;

        // もし、名前が入力されていなかったら、
        // エラーメッセージを表示する。
        if (Name == "")
        {
            NoPlayerNameErrorMessageTextDisPlay();
            return;
        }

        // ルーム名の入力画面が非表示だった場合、表示する。
        // ルーム名の入力画面が表示だった場合、非表示する。
        if(!m_RoomNameInput.activeSelf)
        {
            m_RoomNameInput.SetActive(true);
            m_Select.SetActive(false);
        }
        else if(m_RoomNameInput.activeSelf)
        {
            m_RoomNameInput.SetActive(false);
            m_Select.SetActive(true);
        }
    }

    /// <summary>マッチング</summary>
    public void OnMatingClick()
    {
        // 入力された名前を取得
        string Name = m_NameInputField.text;

        // もし、名前が入力されていなかったら、
        // エラーメッセージを表示する。
        if (Name == "")
        {
            NoPlayerNameErrorMessageTextDisPlay();
            return;
        }

        // 入力されたパスワードを取得
        string RoomName = m_RoomNameInputField.text;
        // 現在、存在するルームの数が0だった場合、
        // エラーメッセージを表示
        if (PhotonManager.Instance.RoomNames().Count == 0)
        {
            NoRoomErrorMessageMessageTextDisPlay();
            return;
        }

        PhotonManager.Instance.OnRandomJoinedRoom();
    }

    /// <summary>ルームに参加(ルーム名)</summary>
    public void OnJointRoomClick()
    {
        // 入力されたパスワードを取得
        string RoomName = m_RoomNameInputField.text;
        // 現在のルームの数が0だった場合
        // ルーム参加画面を非表示にし、
        // エラーメッセージを表示
        if(PhotonManager.Instance.RoomNames().Count == 0)
        {
            // ルーム参加画面が表示されていた場合、非表示にし、
            // セレクト画面に戻る。
            if (m_RoomNameInput.activeSelf)
            {
                m_RoomNameInput.SetActive(false);
                m_Select.SetActive(true);
            }
            // エラーメッセージを表示
            NoRoomErrorMessageMessageTextDisPlay();
            // これ以降の処理を行わない
            return;
        }
    }

    /// <summary>プレイヤー名が入力されていないというエラーメッセージを表示・非表示</summary>
    void NoPlayerNameErrorMessageTextDisPlay()
    {
        // テキストオブジェクトが非表示になってたら、表示する。
        if (m_NoPlayerNameErrorMessageObject.activeSelf == false) m_NoPlayerNameErrorMessageObject.SetActive(true);

        // エラーメッセージのテキストを取得
        // NoPlayerNameErrorMessageを省略
        Text NPNEMText = m_NoPlayerNameErrorMessageObject.GetComponent<Text>();

        // 現在のテキストカラーを取得
        // NoPlayerNameErrorMessageTextを省略
        Color NPNEMTColor = NPNEMText.color;
        // Alpha値に1を入れる。
        NPNEMTColor.a = 1.0f;
        // テキストカラーを反映
         NPNEMText.color = NPNEMTColor;

        // アルファ値が0だった場合
        // エラーメッセージをフェードインする。
        if (NPNEMText.color.a == 1.0f)
        {
            NPNEMText.DOFade(0.0f, 1.0f).OnComplete(() => {
                m_NoPlayerNameErrorMessageObject.SetActive(false);
            });
        }
    }

    /// <summary>ルームが存在しない時のエラーメッセージを表示・非表示</summary>
    void NoRoomErrorMessageMessageTextDisPlay()
    {
        // テキストオブジェクトが非表示になってたら、表示する。
        if (m_NoRoomErrorMessageObject.activeSelf == false) m_NoRoomErrorMessageObject.SetActive(true);

        // エラーメッセージのテキストを取得
        // NoRoomErrorMessageMessageを省略
        Text NREMMText = m_NoRoomErrorMessageObject.GetComponent<Text>();

        // 現在のテキストカラーを取得
        // NoRoomErrorMessageMessageTextColorを省略
        Color NREMMTColor = NREMMText.color;
        // Alpha値に1を入れる。
        NREMMTColor.a = 1.0f;
        // テキストカラーを反映
        NREMMText.color = NREMMTColor;

        // アルファ値が0だった場合
        // エラーメッセージをフェードインする。
        if (NREMMText.color.a == 1.0f)
        {
            NREMMText.DOFade(0.0f, 1.0f).OnComplete(() => {
                m_NoRoomErrorMessageObject.SetActive(false);
            });
        }
    }

    /// <summary>ゲームスタートボタンが押された状態を返す(プロパティ)</summary>
    public bool GameStartButtonClickFrag
    {
        get { return m_GameStartButtonClickFrag; }
        set { m_GameStartButtonClickFrag = value; }
    }
}
