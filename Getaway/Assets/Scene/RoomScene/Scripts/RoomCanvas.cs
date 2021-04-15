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
    /// <summary>プレイヤーの名前のテキスト</summary>
    Text[] m_PlayerNameTexts = new Text[4];
    /// <summary>現在のプレイヤー名</summary>
    string[] m_CurrentPlayerNames;

    void Start()
    {
        // 現在のプレイヤー名を取得
        m_CurrentPlayerNames = PhotonManager.Instance.PlayerNames();
        // 現在のルーム名を取得
        m_RoomNameText.text = PhotonManager.Instance.CurrentRoomName();
        // 最初にプレイヤーを反映
        NumberOfPlayerNamesDisPlay(m_CurrentPlayerNames);
    }

    void Update()
    {
        // プレイヤー名を取得
        string[] PlayerNames = PhotonManager.Instance.PlayerNames();

        // 現在のプレイヤー名の数とプレイヤー名の数を比較して、違いがある場合
        // また、現在のプレイヤー名の数とプレイヤー名の数が4以下だった場合
        // プレイヤー名を表示・非表示にする。
        if(m_CurrentPlayerNames.Length != PlayerNames.Length ||
           m_CurrentPlayerNames.Length <= 4 && PlayerNames.Length <= 4)
        {
            // 現在のプレイヤー名を反映
            m_CurrentPlayerNames = PlayerNames;
            // プレイヤー名を表示する。
            NumberOfPlayerNamesDisPlay(m_CurrentPlayerNames);
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
                m_PlayerNameTexts[PlayerCount].text = PlayerNames[PlayerCount];
            }
        }
    }
}
