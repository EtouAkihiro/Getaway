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
}

public partial class PhotonController
{
    /// <summary>Photonサーバーに接続する</summary>
    public void ConnectedToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
}