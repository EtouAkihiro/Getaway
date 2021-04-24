using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    /// <summary>ゲーム終了オブジェクト</summary>
    public GameObject m_GameOverObject;

    /// <summary>ロード済みかどうか</summary>
    bool m_Loaded = false;

    void Awake()
    {
        // ロード済みだった場合、処理を行わない
        if (m_Loaded) return;

        // ロード済みじゃなかった場合、ロード済みにする。
        m_Loaded = true;
        // シーン遷移で破棄されない
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // 最初は非表示にする。
        m_GameOverObject.SetActive(false);
    }

    void Update()
    {
        // ESCキーを押したら、ゲーム終了確認画面を表示
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // もし、非表示だった場合、表示する。
            if (!m_GameOverObject.activeSelf)
            {
                m_GameOverObject.SetActive(true);
            }
        }
    }

    /// <summary>はいを押された</summary>
    public void YesOnClick()
    {
        // ロビーから切断
        PhotonManager.Instance.LeaveLobby();
        // サーバーから切断
        PhotonManager.Instance.DisconnectSavar();

        // Editorでも終了する。
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
         Application.Quit();
#endif
    }

    /// <summary>いいえを押された</summary>
    public void NoOnClick()
    {
        if (m_GameOverObject.activeSelf)
        {
            m_GameOverObject.SetActive(false);
        }
    }
}
