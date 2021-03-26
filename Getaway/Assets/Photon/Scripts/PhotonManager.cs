using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
