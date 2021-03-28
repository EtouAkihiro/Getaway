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
}

public partial class PhotonController
{
    /// <summary>ルーム一覧</summary>
    List<RoomInfo> m_RoomList = new List<RoomInfo>();

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
        // ルームの作成
        PhotonNetwork.CreateRoom(RoomName, options, TypedLobby.Default);
    }

    /// <summary>現在、参加しているルームのプレイヤー名を取得</summary>
    /// <param name="RoomName"></param>
    /// <returns></returns>
    public string CurrentRoomNames(string RoomName)
    {
        // ルームナンバーを保存
        int RoomNumberSave = 0;

        for(int RoomNumber = 0; RoomNumber <= m_RoomList.Count; RoomNumber++)
        {
            if(m_RoomList[RoomNumber].Name == RoomName)
            {
                RoomNumberSave = RoomNumber;
                break;
            }
        }

        return m_RoomList[RoomNumberSave].ToStringFull();
    }

    /// <summary>ルーム一覧(プロパティ)</summary>
    public List<RoomInfo> RoomList
    {
        get { return m_RoomList; }
        set { m_RoomList = value; }
    }
}