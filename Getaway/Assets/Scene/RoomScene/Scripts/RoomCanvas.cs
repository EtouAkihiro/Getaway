using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

/// <summary>ルームキャンバスの管理を行うクラス</summary>
public class RoomCanvas : SingletonMOnoBehaviour<RoomCanvas>
{
    /// <summary>ルーム名</summary>
    public Text m_RoomNameText;
    /// <summary>プレイヤーの名前</summary>
    public GameObject[] m_PlayerNamesObjects;
    /// <summary>ゲームスタートボタン</summary>
    public GameObject m_GamePlayButton;
    /// <summary>プレイヤー</summary>
    Photon.Realtime.Player[] m_Players;
    /// <summary>プレイヤーの名前のテキスト</summary>
    Text[] m_PlayerNameTexts;
    /// <summary>現在のプレイヤー名</summary>
    string[] m_CurrentPlayerNames;

    void Start()
    {
        // ルーム限界参加人数分のプレイヤー名のテキストを設定
        m_PlayerNameTexts = new Text[PhotonNetwork.CurrentRoom.MaxPlayers];
        // 現在のプレイヤーを取得
        m_Players = PhotonNetwork.PlayerList;
        // 現在のプレイヤー名を取得
        m_CurrentPlayerNames = PlayerNames(m_Players);
        // 現在のルーム名を取得
        m_RoomNameText.text = PhotonManager.Instance.CurrentRoomName();
        // 最初にプレイヤーを反映
        NumberOfPlayerNamesDisPlay(m_CurrentPlayerNames);
        // ルームマスターじゃあないだった場合、ゲームプレイボタンを非表示にする。
        if (!PhotonNetwork.IsMasterClient) m_GamePlayButton.SetActive(false);
    }

    void Update()
    {
        // プレイヤー名を取得
        string[] playernames = PlayerNames(m_Players);
        // 現在のルーム内にいるプレイヤー数が 0 以下だった場合、それ以降の処理を行わない
        if (PhotonNetwork.PlayerList.Length <= 0 || PhotonNetwork.PlayerList == null) return;

        // 保存したプレイヤー参加数と現在の参加数が異なり、
        // 保存したプレイヤー感か数がルームの限界参加人数より少なかった場合
        if(m_Players != PhotonNetwork.PlayerList &&
           m_Players.Length < PhotonNetwork.CurrentRoom.MaxPlayers + 1)
        {
            // プレイヤーを反映
            m_Players = PhotonNetwork.PlayerList;
            // 現在のプレイヤー名を反映
            m_CurrentPlayerNames = playernames;
            // プレイヤー名を表示する。
            NumberOfPlayerNamesDisPlay(m_CurrentPlayerNames);
            // 現在のルームを取得
            Photon.Realtime.Room room = PhotonNetwork.CurrentRoom;
            // ルーム内にいるプレイヤー数が現在のルームに入れるプレイヤー数以上だった場合、
            // ルームを閉じる。
            if ( room.PlayerCount >= room.MaxPlayers)
            {
                // ルームを閉じる
                room.IsOpen = false;
            }
            else
            {
                // ルームを開ける
                room.IsOpen = true;
            }
        }
    }

    /// <summary>参加プレイヤー人数分のプレイヤー名を表示</summary>
    /// <param name="PlayerNames">プレイヤー名</param>
    public void NumberOfPlayerNamesDisPlay(string[] PlayerNames)
    {

        // 参加していない分のテキストを非表示にする。
        // 表示する分のテキストを取得します。
        for(int PlayerCount = 0; PlayerCount < m_PlayerNamesObjects.Length; PlayerCount++)
        {
            // 表示しないものは非表示にする
            if(PlayerCount >= PhotonNetwork.CountOfPlayers)
            {
                m_PlayerNamesObjects[PlayerCount].SetActive(false);
            }
            // 表示する分のテキストを取得
            else
            {
                // もし、非表示になっている場合、行事するようにします。
                if (!m_PlayerNamesObjects[PlayerCount].active)
                {
                    m_PlayerNamesObjects[PlayerCount].SetActive(true);
                }

                m_PlayerNameTexts[PlayerCount] = m_PlayerNamesObjects[PlayerCount].GetComponent<Text>();
            }

            if (m_PlayerNamesObjects[PlayerCount].active)
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    m_PlayerNameTexts[PlayerCount].text = PlayerNames[PlayerCount];
                }
                else
                {
                    m_PlayerNameTexts[PlayerCount].text = PlayerNames[PlayerCount];
                }
            }
        }
    }

    /// <summary>現在のルームにいるプレイヤー名を返す</summary>
    /// <returns></returns>
    string[] PlayerNames(Photon.Realtime.Player[] players)
    {
        // プレイヤー名一覧
        string[] PlayerNames = new string[PhotonNetwork.CountOfPlayers];

        // プレイヤーリストを取得
        Photon.Realtime.Player[] Players = PhotonNetwork.PlayerList;

        // 現在のプレイヤー名を取得
        for (int PlayerNumber = 0; PlayerNumber < Players.Length; PlayerNumber++)
        {
            PlayerNames[PlayerNumber] = Players[PlayerNumber].NickName;
        }

        // もしプレイヤー一覧の中身がなかった場合、nullで返す
        if (PlayerNames == null) return null;

        // もしプレイヤー一覧の中身があった場合、プレイヤー一覧を返す
        return PlayerNames;

        // もし、何も追加されていない場合、nullで返す。
        if (PlayerNames == null) return null;

        // 現在、参加しているプレイヤー分の名前を返す。
        return PlayerNames;
    }
}
