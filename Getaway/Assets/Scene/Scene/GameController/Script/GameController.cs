using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>データの共有</summary>
public class GameController : SingletonMOnoBehaviour<GameController>
{
    /// <summary>ロードの状態</summary>
    bool m_Loading = false;
    /// <summary>ルームから退出</summary>
    bool m_RoomExit = false;

    /// <summary>スクリプトのインスタンスがロードされたときに呼び出されます(Unityドキュメント参照)</summary>
    void Awake()
    {
        // ロードされている状態だった場合
        // 処理を行わない
        if (m_Loading) return;

        // ロードされていない場合
        // ロードされた状態にする。
        m_Loading = true;
        // シーン切替時、破棄されない。
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>ルーム退出(プロパティ)</summary>
    public bool RoomExit
    {
        get { return m_RoomExit; }
        set { m_RoomExit = value; }
    }
}
