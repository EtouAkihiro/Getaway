using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    /// <summary>オーディオソース</summary>
    AudioSource m_AudioSource;
    /// <summary>ロード済みかどうか</summary>
    bool m_Loaded = false;

    void Awake()
    {
        // ロード済みだった場合、処理を行わない
        if (m_Loaded) return;
        // ロード済みにする。
        m_Loaded = true;
        // 全シーンにまたがって存在する（シーン切り替えで破壊されない）ようにする
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // オーディオソースを参照
        m_AudioSource = GetComponent<AudioSource>();
    }
}
