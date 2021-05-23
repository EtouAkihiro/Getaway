using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>データの共有</summary>
public class GameController : SingletonMOnoBehaviour<GameController>
{
    /// <summary>ルームから退出</summary>
    bool m_RoomExit = false;

    /// <summary>スクリプトのインスタンスがロードされたときに呼び出されます(Unityドキュメント参照)</summary>
    void Awake()
    {
        // 現在あるGameControllerタグを持っているオブジェクトを取得
        GameObject[] GameControllers = GameObject.FindGameObjectsWithTag("GameController");
        // GameControllerタグを持っているオブジェクトが2個以上ある場合、
        // 新しく生成されたオブジェクトを削除する。
        if (GameControllers.Length >= 2)
        {
            for (int i = 0; i < GameControllers.Length; i++)
            {
                if (i != 0)
                {
                    Destroy(GameControllers[i]);
                }
            }
        }
        // ２個未満だった場合、
        // シーン遷移で破棄されないオブジェクトにする。
        else
        {
            // シーン切り替えで破棄されない。
            DontDestroyOnLoad(gameObject);
        }
    }

    /// <summary>ルーム退出(プロパティ)</summary>
    public bool RoomExit
    {
        get { return m_RoomExit; }
        set { m_RoomExit = value; }
    }
}
