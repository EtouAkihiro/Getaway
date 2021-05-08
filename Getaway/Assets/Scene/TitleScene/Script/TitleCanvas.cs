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
    /// <summary>入力されたルーム名が存在しないというエラーメッセージのオブジェクト</summary>
    public GameObject m_InputNoRoomNameErrorMessageObject;
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
    /// <summary>参加人数の入力</summary>
    public InputField m_TheNumberOfParticipantsInputField;

    /// <summary>シーンの状態</summary>
    public enum SceneState
    {
        Title = 0,
        Select = 1,
    }

    /// <summary>シーンの状態</summary>
    public SceneState m_SceneState = SceneState.Title;

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

        // ルーム退出した場合
        if (GameController.Instance.RoomExit)
        {
            // タイトルオブジェクトを非表示にする
            m_Title.SetActive(false);
            // セレクト画面を表示するアニメーションを再生
            SelectCanvasFade(true);
        }
        else
        {
            // セレクトオブジェクトを非表示にする
            m_Select.SetActive(false);
            // タイトル画面を表示するアニメーションを再生
            TitleCanvasFade(true);
        }
    }

    /// <summary>タイトルのフェードをする(true:フェードアウト, false:フェードイン)</summary>
    /// <param name="Frag">フラグ</param>
    public void TitleCanvasFade(bool Frag)
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

        // シーンの状態
        if (m_Title.activeSelf) m_SceneState = SceneState.Title;
        if (m_Select.activeSelf) m_SceneState = SceneState.Select;
    }

    /// <summary> ゲームスタートボタンが押された時</summary>
    public void OnGameStartClick()
    {
        // ゲームスタートボタンのアニメーションを再生
        m_GameStartButtonScript.Play_OnClick_AnimatorBlinking();
        // ボタンを消す
        TitleCanvasFade(false);
        // セレクト画面を表示
        SelectCanvasFade(true);
        // サーバーに接続
        PhotonManager.Instance.ConnectedToServer();
        // ロビーに接続
        PhotonManager.Instance.JointLobby();
        // 押された状態にする。
        m_GameStartButtonClickFrag = true;
        // セーブしたプレイヤー名を取得
        string PlayerNameSaveData = PlayerPrefs.GetString("PlayerNameData");
        // セーブしたプレイヤー名がある場合、
        // 最初からプレイヤー名が入力された状態にする。
        if (PlayerNameSaveData != "")
        {
            m_NameInputField.text = PlayerNameSaveData;
        }
    }

    /// <summary>タイトルに戻る</summary>
    public void OnBackToTitle()
    {
        // セレクト画面を非表示
        SelectCanvasFade(false);
        // タイトルを表示
        TitleCanvasFade(true);
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
        // セーブしたプレイヤー名を取得
        string PlayerNameSaveData = PlayerPrefs.GetString("PlayerNameData");

        // もし、プレイヤー名がセーブされていない、
        // または、セーブしたプレイヤー名と異なる場合
        // プレイヤー名をセーブする。
        if(PlayerNameSaveData == ""　|| PlayerName != PlayerNameSaveData)
        {
            PlayerPrefs.SetString("PlayerNameData", PlayerName);
        }

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
        // 参加人数を取得
        byte TheNumberOfParticipants = byte.Parse(m_TheNumberOfParticipantsInputField.text);
        // ルーム名が入力されていなかったら、シーン遷移しない
        if (RoomName == "") return;
        // ルームの参加人数が想定より大きくなった場合、シーン遷移しない
        if (TheNumberOfParticipants > 4) return;

        // ルームを作成
        PhotonManager.Instance.CreateRoom(RoomName, TheNumberOfParticipants);
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
        // 現在のルーム名のリストを取得
        List<string> RoomNameList = PhotonManager.Instance.RoomNames();
        // 現在、存在するルームの数が0以下だった場合、
        // エラーメッセージを表示
        if (RoomNameList.Count <= 0)
        {
            NoRoomErrorMessageTextDisPlay();
            return;
        }
        // 現在、存在するルームの数が0より大きかっただった場合、
        // ランダムマッチを行う
        else if (RoomNameList.Count > 0)
        {
            // ランダムマッチを行う。
            PhotonManager.Instance.OnRandomJoinedRoom();

            if (PhotonNetwork.InRoom)
            {
                // ルームシーンに遷移
                Fade.Instance.FadeOut("RoomScene");
            }
            else
            {
                Debug.LogError("ルームに入れませんでした");
            }
        }
    }

    /// <summary>ルームに参加(ルーム名)</summary>
    public void OnJointRoomClick()
    {
        // ルームカウンター
        int RoomCount = 0;
        // 入力されたパスワードを取得
        string RoomName = m_RoomNameInputField.text;
        // 現在のルーム名のリストを取得
        List<string> RoomNameList = PhotonManager.Instance.RoomNames();
        // 現在のルームの数が0以下だった場合
        // ルーム参加画面を非表示にし、
        // エラーメッセージを表示
        if (RoomNameList.Count <= 0)
        {
            // ルーム参加画面が表示されていた場合、非表示にし、
            // セレクト画面に戻る。
            if (m_RoomNameInput.activeSelf)
            {
                m_RoomNameInput.SetActive(false);
                m_Select.SetActive(true);
            }
            // エラーメッセージを表示
            NoRoomErrorMessageTextDisPlay();
            // これ以降の処理を行わない
            return;
        }
        // 現在のルームが0より大きい場合、
        // ルームに参加する
        else if(RoomNameList.Count > 0)
        {
            foreach(string RoomNames in RoomNameList)
            {
                // 現在のルームのルーム名と入力されたルーム名がどれか、一致したら
                // ルームシーンに遷移する。
                if(RoomName == RoomNames)
                {
                    // ルームに参加
                    PhotonManager.Instance.OnJoinedRoom(RoomName);
                    // ルームシーンに遷移
                    Fade.Instance.FadeOut("RoomScene");
                    // foreachから抜ける
                    break;
                }
                // 現在のルームのルーム名と入力されたルーム名がどれかも一致しなかったら、
                // ルームカウンターに加算
                else
                {
                    RoomCount++;
                }
            }

            // ルームカンターが現在のルーム名の数以上ある場合
            // ルーム参加画面を非表示にし、
            // エラーメッセージを表示
            if(RoomCount >= RoomNameList.Count)
            {
                // ルーム参加画面が表示されていた場合、非表示にし、
                // セレクト画面に戻る。
                if (m_RoomNameInput.activeSelf)
                {
                    m_RoomNameInput.SetActive(false);
                    m_Select.SetActive(true);
                }

                // エラーメッセージを表示
                InputNoRoomNameErrorMessageTextDisPlay();
                // これ以降の処理を行わない
                return;
            }
        }
    }

    /// <summary>プレイヤー名が入力されていないというエラーメッセージを表示・非表示</summary>
    void NoPlayerNameErrorMessageTextDisPlay()
    {
        // テキストオブジェクトが非表示になってたら、表示する。
        if (!m_NoPlayerNameErrorMessageObject.activeSelf) m_NoPlayerNameErrorMessageObject.SetActive(true);

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
    void NoRoomErrorMessageTextDisPlay()
    {
        // テキストオブジェクトが非表示になってたら、表示する。
        if (!m_NoRoomErrorMessageObject.activeSelf) m_NoRoomErrorMessageObject.SetActive(true);

        // エラーメッセージのテキストを取得
        // NoRoomErrorMessageを省略
        Text NREMText = m_NoRoomErrorMessageObject.GetComponent<Text>();

        // 現在のテキストカラーを取得
        // NoRoomErrorMessageMessageTextColorを省略
        Color NREMTColor = NREMText.color;
        // Alpha値に1を入れる。
        NREMTColor.a = 1.0f;
        // テキストカラーを反映
        NREMText.color = NREMTColor;

        // アルファ値が0だった場合
        // エラーメッセージをフェードインする。
        if (NREMText.color.a == 1.0f)
        {
            // 徐々に消える
            NREMText.DOFade(0.0f, 1.0f).OnComplete(() => {
                // 消えたら、非表示にする。
                m_NoRoomErrorMessageObject.SetActive(false);
            });
        }
    }

    /// <summary>入力されたルーム名が存在しない時のエラーメッセージを表示・非表示</summary>
    void InputNoRoomNameErrorMessageTextDisPlay()
    {
        // テキストオブジェクトが非表示になっていたら、表示する。
        if (!m_InputNoRoomNameErrorMessageObject.activeSelf) m_InputNoRoomNameErrorMessageObject.SetActive(true);

        // エラーメッセージのテキストを取得
        // InputNoRoomNameErrorMessageを省略
        Text INPNEMText = m_InputNoRoomNameErrorMessageObject.GetComponent<Text>();

        // 現在のテキストカラーを取得
        // InputNoRoomNameErrorMessageTextを省略
        Color INPNEMTColor = INPNEMText.color;
        // Alpha値に1を入れる。
        INPNEMTColor.a = 1.0f;
        // テキストカラーを反映
        INPNEMText.color = INPNEMTColor;
        // Alpha値が1だった場合
        // エラーメッセージをフェードインする。
        if (INPNEMText.color.a == 1.0f)
        {
            // 徐々に消える
            INPNEMText.DOFade(0.0f, 1.0f).OnComplete(() => {
                // 消えたら、非表示にする。
                m_InputNoRoomNameErrorMessageObject.SetActive(false);
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
