using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

/// <summary>Photon管理クラス</summary>
public partial　class PhotonController : MonoBehaviourPunCallbacks
{
    /// <summary>Photonサーバーに接続1(コールバック)</summary>
    public override void OnConnected()
    {
        base.OnConnected();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
    }

    /// <summary>Photonサーバーに接続2(コールバック)</summary>
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
    }

    /// <summary>切断時</summary>
    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
    }

    /// <summary>ルームを作成した</summary>
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        // ルーム名を取得
        RoomName = PhotonNetwork.CurrentRoom.Name;
    }

    /// <summary>ルーム作成に失敗した</summary>
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
    }

    /// <summary>現在のルーム一覧の更新</summary>
    /// <param name="roomList">ルーム一覧</param>
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        // ルーム一覧を取得
        RoomList = roomList;
    }

    /// <summary>ルームにプレイヤーが入ってきた</summary>
    /// <param name="newPlayer">入ってきたプレイヤー</param>
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
    }
}

public partial class PhotonController
{
    /// <summary>ルームインフォ</summary>
    RoomInfo m_RoomInfo;
    

    /// <summary>ルーム一覧</summary>
    List<RoomInfo> m_RoomList = new List<RoomInfo>();
    /// <summary>ルーム名</summary>
    string m_RoomName;

    /// <summary>Photonサーバーに接続する</summary>
    public void ConnectedToServer()
    {
        // サーバーに接続されている場合は、処理を行わない
        if (PhotonNetwork.IsConnected) return;

        // サーバーに接続
        PhotonNetwork.ConnectUsingSettings();
    }

    /// <summary>Photonサーバーから切断する</summary>
    public void DisconnectSavar()
    {
        // サーバーに接続されていない場合は、処理を行わない
        if (!PhotonNetwork.IsConnected) return;

        // サーバーから切断する
        PhotonNetwork.Disconnect();
    }

    /// <summary>ルームを作成</summary>
    /// <param name="RoomName">ルーム名</param>
    public void CreateRoom(string RoomName)
    {
        // ルームオプションを作成
        RoomOptions options = new RoomOptions();
        // ルームのプレイ人数を4人に設定
        options.MaxPlayers = 4;
        //ルームの入室を許可
        options.IsOpen = true;
        // ルームの作成
        PhotonNetwork.CreateRoom(RoomName, options, TypedLobby.Default);
    }

    /// <summary>ルーム参加(パスワード)</summary>
    /// <param name="passwad">パスワード</param>
    public void OnJoinedRoom(string passwad)
    {
    }

    /// <summary>ルーム参加(ランダム)</summary>
    public void OnRandomJoinedRoom()
    {
    }

    /// <summary>ルームから退出</summary>
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }


    /// <summary>プレイヤーの名前を登録</summary>
    /// <param name="PlayerName">プレイヤーの名前</param>
    public void SetPlayerName(string PlayerName)
    {
        // プレイヤー名を登録
        PhotonNetwork.NickName = PlayerName;
    }

    /// <summary>現在使用されているルーム分のルーム名を返します。</summary>
    /// <returns></returns>
    public List<string> RoomNames()
    {
        // 現在使用されている分を確保する。
        List<string> RoomNames = new List<string>();

        // 現在ルーム分のルーム名を取得
        for(int Roomid = 0; Roomid < PhotonNetwork.CountOfRooms; Roomid++)
        {
            RoomNames[Roomid] = m_RoomList[Roomid].Name;
        }

        // 現在使用されているルーム分のルーム名を返します。
        return RoomNames;
    }

    /// <summary>現在のルームにいるプレイヤー名を返す</summary>
    /// <returns></returns>
    public string[] PlayerNames()
    {
        // 現在のルームにいるプレイヤー分を確保する。
        string[] PlayerNames = new string[m_RoomInfo.PlayerCount];

        // 現在のルームにいるプレイヤー人数と
        // ルームの制限人数とが、異なる場合
        // ルームにいるプレイヤー分の名前を取得
        if(m_RoomInfo.PlayerCount != m_RoomInfo.MaxPlayers)
        {
            for (int id = 0; id < m_RoomInfo.PlayerCount; id++)
            {
                PlayerNames[id] = PhotonNetwork.PlayerList[id].NickName;
            }
        }

        // もし、何も追加されていない場合、nullで返す。
        if (PlayerNames == null) return null;

        // 現在、参加しているプレイヤー分の名前を返す。
        return PlayerNames;
    }

    /// <summary>ルーム一覧(プロパティ)</summary>
    public List<RoomInfo> RoomList
    {
        get { return m_RoomList; }
        set { m_RoomList = value; }
    }

    /// <summary>ルーム名(プロパティ)</summary>
    public string RoomName
    {
        get { return m_RoomName; }
        set { m_RoomName = value; }
    }
}