using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    // オーディオソース
    AudioSource m_AudioSource;

    void Awake() {
        // 全シーンにまたがって存在する（シーン切り替えで破壊されない）ようにする
        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        // オーディオソースを参照
        m_AudioSource = GetComponent<AudioSource>();
    }

    void Update() {
        // 常にSEマネージャーを管理
        SEManagerTwoOrmore();
    }

    /// <summary>SEマネージャーが2個以上存在する場合、新しく生成されたSEマネージャーを削除</summary>
    void SEManagerTwoOrmore() {
        // 現在のシーンにあるBGMマネージャーを取得
        GameObject[] SEManagers = GameObject.FindGameObjectsWithTag("SEManager");

        // BGMマネージャーが2個以上存在する場合
        if (SEManagers.Length >= 2) {
            // 新しく生成されたBGMマネージャーを削除
            Destroy(SEManagers[SEManagers.Length - 1]);
        }
    }
}
