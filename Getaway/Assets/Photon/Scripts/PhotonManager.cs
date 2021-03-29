using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : SingletonMOnoBehaviour<PhotonManager>
{
    /// <summary>Photonコントローラー</summary>
    PhotonController m_PhotonController;

    /// <summary>ロード済みかどうか</summary>
    bool m_Loaded = false;

    void Awake()
    {
        // ロード済みだった場合、何もしない
        if (m_Loaded) return;

        // ロード済みにする
        m_Loaded = true;

        // ロード済みじゃなかった場合は、
        // シーン遷移で破棄されない。
        DontDestroyOnLoad(gameObject);
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

    /// <summary>ルームを作成</summary>
    /// <param name="RoomName">ルーム名</param>
    public void CreateRoom(string RoomName)
    {
        m_PhotonController.CreateRoom(RoomName);
    }

    /// <summary>ルーム名を返します</summary>
    /// <returns></returns>
    public string CurrentRoomName()
    {
        return m_PhotonController.RoomName;
    }
}
