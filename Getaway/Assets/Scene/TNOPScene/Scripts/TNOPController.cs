using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNOPController : MonoBehaviour
{
    /// <summary>フェードの参照</summary>
    Fade m_Fade;

    void Start() {
        // フェードオブジェクトの取得
        m_Fade = GameObject.FindGameObjectWithTag("Fade").GetComponent<Fade>();
        // フェードイン
        m_Fade.FadeIn();
    }

    void Update() {    
    }
}
