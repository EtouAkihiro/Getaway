using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>ルームキャンバスの管理を行うクラス</summary>
public class RoomCanvas : MonoBehaviour
{
    /// <summary>ルーム名</summary>
    public Text m_RoomNameText;
    /// <summary>プレイヤーの名前</summary>
    public GameObject[] m_PlayerNames;
    /// <summary>プレイヤーの名前のテキスト</summary>
    Text[] m_PlayerNameTexts;

    void Start()
    {
        m_RoomNameText.text = PhotonManager.Instance.CurrentRoomName();
    }

    /// <summary>参加プレイヤー人数分のプレイヤー名を表示</summary>
    void NumberOfPlayerNamesDisPlay()
    {
        // プレイヤー名のテキストを取得
        for(int i = 0; i <= m_PlayerNames.Length; i++)
        {
            m_PlayerNameTexts[i] = m_PlayerNames[i].GetComponent<Text>();
        }
    }
}
