using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>タイトルキャンバスの管理</summary>
public class TitleCanvas : MonoBehaviour
{
    /// <summary>エラーメッセージのオブジェクト</summary>
    public GameObject m_WarningTextObject;
    /// <summary>タイトルオブジェクト</summary>
    public GameObject m_Title;
    /// <summary>セレクトオブジェクト</summary>
    public GameObject m_Select;
    /// <summary>ゲームスタート</summary>
    public GameObject m_GameStart;
    /// <summary>ゲームスタートボタン</summary>
    public GameObject m_GameStartButton;
    /// <summary>ルームパスワードを入力する画面</summary>
    public GameObject m_RoomPaswadInput;
    /// <summary>ルーム作成画面</summary>
    public GameObject m_RoomCreationInput;

    /// <summary>名前の入力</summary>
    public InputField m_NameInputField;
    /// <summary>パスワードの入力</summary>
    public InputField m_PaswadInputField;
    /// <summary>ルーム名の入力</summary>
    public InputField m_RoomInputField;

    /// <summary>ゲームスタートボタンのスクリプトの参照</summary>
    GameStartButton m_GameStartButtonScript;

    /// <summary>アニメーター</summary>
    Animator m_Animator;

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
    }

    /// <summary>タイトルに戻る</summary>
    public void OnBackToTitle()
    {
        // セレクト画面を非表示
        SelectCanvasFade(false);
        // タイトルを表示
        TltleCanvasFade(false);
        // サーバーから切断
        PhotonManager.Instance.DisconnectSavar();
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
            WarningTextDisPlay();
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
    public void OnRoomPasswordClick()
    {
        // 入力された名前を取得
        string Name = m_NameInputField.text;

        // もし、名前が入力されていなかったら、
        // エラーメッセージを表示する。
        if (Name == "")
        {
            WarningTextDisPlay();
            return;
        }

        // パスワードの入力画面が非表示だった場合、表示する。
        // パスワードの入力画面が表示だった場合、非表示する。
        if(!m_RoomPaswadInput.activeSelf)
        {
            m_RoomPaswadInput.SetActive(true);
            m_Select.SetActive(false);
        }
        else if(m_RoomPaswadInput.activeSelf)
        {
            m_RoomPaswadInput.SetActive(false);
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
            WarningTextDisPlay();
            return;
        }

        // 入力されたパスワードを取得
        string Paswad = m_PaswadInputField.text;

        // もし、パスワード画面が表示されていて、
        // パスワードが入力された場合
        // パスワードを保存する。
        if (Paswad != "" && m_RoomPaswadInput.activeSelf)
        {
            GameController.Instance.Paswad = Paswad;
        }

        if(Paswad == "")
        {
            PhotonManager.Instance.OnRandomJoinedRoom();
        }
    }

    /// <summary>エラーメッセージを表示・非表示</summary>
    void WarningTextDisPlay()
    {
        // テキストオブジェクトが非表示になってたら、表示する。
        if (m_WarningTextObject.activeSelf == false) m_WarningTextObject.SetActive(true);

        // エラーメッセージのテキストを取得
        Text WarningText = m_WarningTextObject.GetComponent<Text>();

        // 現在のテキストカラーを取得
        Color WarningTextColor = WarningText.color;
        // Alpha値に1を入れる。
        WarningTextColor.a = 1.0f;
        // テキストカラーを反映
        WarningText.color = WarningTextColor;

        // アルファ値が0だった場合
        // エラーメッセージをフェードインする。
        if (WarningText.color.a == 1.0f)
        {
            WarningText.DOFade(0.0f, 1.0f).OnComplete(() => {
                m_WarningTextObject.SetActive(false);
            });
        }
    }
}
