using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoloStageSelectController : MonoBehaviour
{

    enum State
    {
        Laboratory
    }

    /// <summary>状態</summary>
    State m_State;


    /// <summary>フェードのスクリプトを参照</summary>
    Fade m_FadeScript;

    void Start() {
        // フェードのスクリプトを取得
        m_FadeScript = GameObject.FindGameObjectWithTag("Fade").GetComponent<Fade>();
        // フェードイン
        m_FadeScript.FadeIn();
        // 状態を設定
        m_State = State.Laboratory;
    }

    void Update() {
        // 状態ごとに更新
        switch (m_State)
        {
            case State.Laboratory : LaboratoryUpdate(); break; 
        }
    }

    // 研究所
    void LaboratoryUpdate() {

    }
}
