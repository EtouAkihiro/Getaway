using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    [SerializeField, Header("タイトルBGM")]
    private AudioClip m_TitleBGM;

    /// <summary>ロード済みかどうか</summary>
    bool m_Loaded = false;

    // オーディオソース
    AudioSource m_AudioSource;

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

    void Update()
    {
        // 現在のシーン名を取得
        string SceneName = SceneManager.GetActiveScene().name;
        // シーンごとにBGMの再生を行う
        SceneUpdate(SceneName);
    }

    /// <summary>シーンごとにBGMの再生を行う</summary>
    /// <param name="SceneName"></param>
    void SceneUpdate(string SceneName)
    {
        // シーンごとにBGMの再生
        switch (SceneName)
        {
        }
    }

    /// <summary>BGMの再生</summary>
    /// <param name="audioClip">サウンドクリップ</param>
    void PlayBGM(AudioClip audioClip)
    {
        // 現在のBGMと同じBGMじゃあない場合
        // または、クリップの中に何もなかった場合
        if(m_AudioSource.clip != audioClip ||
           m_AudioSource.clip == null)
        {
            // 入れ替えて、再生
            m_AudioSource.clip = audioClip;
            m_AudioSource.Play();
        }
    }

    /// <summary>BGMの停止</summary>
    void StopBGM()
    {
        // もし、BGMの再生されていた場合
        if (m_AudioSource.isPlaying) {
            // BGMの停止
            m_AudioSource.Stop();
            // サウンドクリップの中身を空にする
            m_AudioSource.clip = null;
        }
    }
}
