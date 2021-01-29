using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNOPController : MonoBehaviour
{
    /// <summary>フェードオブジェクト</summary>
    GameObject m_Fade;

    void Start() {
        // フェードオブジェクトの取得
        m_Fade = GameObject.FindGameObjectWithTag("Fade");
        // フェードイン
        m_Fade.GetComponent<Fade>().FadeIn();
    }

    void Update() {    
    }
}
