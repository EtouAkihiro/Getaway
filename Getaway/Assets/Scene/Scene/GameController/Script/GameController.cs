using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : SingletonMOnoBehaviour<GameController>
{
    /// <summary>ロードの状態</summary>
    bool m_Loading = false;
    /// <summary>パスワード</summary>
    string m_Paswad;

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

    /// <summary>パスワード(プロパティ)</summary>
    public string Paswad
    {
        get { return m_Paswad; }
        set { m_Paswad = value; }
    }
}
