using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoloStageSelectController : MonoBehaviour
{
    /// <summary>研究所のカメラの位置</summary>
    public GameObject m_LaboratoryCameraPoint;
    /// <summary>最初に選択されるオブジェクト</summary>
    public GameObject m_StartSelectObject;
    /// <summary>カメラの回転スピード</summary>
    public float m_StageCameraRotateSpeed = 0.0f;

    /// <summary>ステージの番号</summary>
    int m_StageNumber = 0;

    enum State
    {
        Laboratory
    }

    /// <summary>状態</summary>
    State m_State;

    /// <summary>メインカメラ</summary>
    GameObject m_StageCamera;

    /// <summary>フェードのスクリプトを参照</summary>
    Fade m_FadeScript;

    void Start()
    {
        // フェードのスクリプトを取得
        m_FadeScript = GameObject.FindGameObjectWithTag("Fade").GetComponent<Fade>();
        // ステージカメラの取得
        m_StageCamera = GameObject.Find("StageCamera");
        // フェードイン
        m_FadeScript.FadeIn();
        // 状態を設定
        m_State = State.Laboratory;
    }

    void Update()
    {
        // 状態ごとに更新
        switch (m_State)
        {
            case State.Laboratory : LaboratoryUpdate(); break; 
        }

        // カメラの更新
        MainCameraUpdate(m_State);
    }

    /// <summary>状態ごとにカメラの更新</summary>
    /// <param name="state">状態</param>
    void MainCameraUpdate(State state)
    {
        // 研究所が選択されていた場合
        if(state == State.Laboratory)
        {
            // カメラの位置を研究所のカメラの位置を設定
            m_StageCamera.transform.position = m_LaboratoryCameraPoint.transform.position;
            // カメラの回転
            m_StageCamera.transform.eulerAngles += new Vector3(0.0f, 1.0f, 0.0f) * m_StageCameraRotateSpeed * Time.deltaTime;
        }
    }

    /// <summary>研究所が選択されたときの更新</summary>
    void LaboratoryUpdate()
    {
    }

    /// <summary>研究所がクリックされた時</summary>
    public void OnClick_Laboratory()
    {
        // ステージの番号を指定(1を指定)
        m_StageNumber = 1;
        // シーンチェンジ
        m_FadeScript.FadeOut("Solo_GamePlay_LaboratoryScene");

    }

    /// <summary>ステージの番号を返す</summary>
    /// <returns></returns>
    public int isStageNumber()
    {
        return m_StageNumber;
    }
}
