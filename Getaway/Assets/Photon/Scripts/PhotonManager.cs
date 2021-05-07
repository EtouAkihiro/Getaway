using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : SingletonMOnoBehaviour<PhotonManager>
{
    /// <summary>Photonコントローラー</summary>
    PhotonController m_PhotonController;

    void Awake()
    {
        // 現在あるPhotonControllerタグを持っているオブジェクトを取得
        GameObject[] PhotonControllers = GameObject.FindGameObjectsWithTag("PhotonController");
        // Photonタグを持っているオブジェクトが2個以上ある場合、
        // 新しく生成されたオブジェクトを削除する。
        if (PhotonControllers.Length >= 2)
        {
            for(int i = 0; i < PhotonControllers.Length; i++)
            {
                if(i != 0)
                {
                    Destroy(PhotonControllers[i]);
                }
            }
        }
        // ２個未満だった場合、
        // シーン遷移で破棄されないオブジェクトにする。
        else
        {
            // シーン遷移で破棄されない。
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        // Photonコントローラーを参照
        m_PhotonController = GetComponent<PhotonController>();
    }

    /// <summary>サーバーに接続</summary>
    public void ConnectedToServer()
    {
        m_PhotonController.ConnectedToServer();
    }

    /// <summary>サーバーから切断</summary>
    public void DisconnectSavar()
    {
        m_PhotonController.DisconnectSavar();
    }

    /// <summary>ロビーに接続</summary>
    public void JointLobby()
    {
        m_PhotonController.JointLobby();
    }

    /// <summary>ロビーから切断</summary>
    public void LeaveLobby()
    {
        m_PhotonController.LeaveLobby();
    }

    /// <summary>ルームを作成</summary>
    /// <param name="RoomName">ルーム名</param>
    /// <param name="MaxPlayers">参加人数</param>
    public void CreateRoom(string RoomName, byte MaxPlayers)
    {
        m_PhotonController.CreateRoom(RoomName, MaxPlayers);
    }

    /// <summary>ルームから退出</summary>
    public void LeaveRoom()
    {
        m_PhotonController.LeaveRoom();
    }

    /// <summary>ルームに参加(ルーム名)</summary>
    /// <param name="RoomName">ルーム名</param>
    public void OnJoinedRoom(string RoomName)
    {
        m_PhotonController.OnJoinedRoom(RoomName);
    }

    /// <summary>ルームに参加(ランダム)</summary>
    public void OnRandomJoinedRoom()
    {
        m_PhotonController.OnRandomJoinedRoom();
    }

    /// <summary>プレイヤー名を登録</summary>
    /// <param name="PlayerName">登録したいプレイヤー名</param>
    public void SetPlayerName(string PlayerName)
    {
        m_PhotonController.SetPlayerName(PlayerName);
    }

    /// <summary>現在使用されているルーム分のルーム名を返します。</summary>
    /// <returns></returns>
    public List<string> RoomNames()
    {
        return m_PhotonController.RoomNames();
    }

    /// <summary>ルームの一覧を返す。</summary>
    /// <returns></returns>
    public List<RoomInfo> RoomList()
    {
        return m_PhotonController.RoomList;
    }

    /// <summary>ルーム名を返します</summary>
    /// <returns></returns>
    public string CurrentRoomName()
    {
        return m_PhotonController.RoomName;
    }
}
