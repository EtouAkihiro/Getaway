using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    // Fadeオブジェクト
    GameObject m_Fade;

    void Start() {
        // Fadeオブジェクトを取得
        m_Fade = GameObject.FindGameObjectWithTag("Fade");
    }

    void Update() {   
    }
}
