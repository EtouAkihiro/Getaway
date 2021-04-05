using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>ルームシーン内のボタンを管理するクラス</summary>
public class RoomButtonController : MonoBehaviour
{
    /// <summary>選択されるステージのテキストのオブジェクト</summary>
    public GameObject[] m_SelectStageTextObjects;

    /// <summary>表示されているかのフラグ</summary>
    bool m_DisPlayFrag = false;
    /// <summary>非表示になっているかのフラグ</summary>
    bool m_DontDisPlayFrag = false;

    /// <summary>選択できるステージの状態</summary>
    enum StageSelectStage
    {
        Random,
        Underground
    }

    /// <summary>選択されているステージの状態</summary>
    StageSelectStage m_StageSelectStage;

    void Start()
    {
        // 最初はRandomを選択した状態にする。
        m_StageSelectStage = StageSelectStage.Random;
        // 地下のテキストオブジェクトを非表示にする
        m_SelectStageTextObjects[1].SetActive(false);

    }

    /// <summary>ステージ選択の左側のボタンが押された時</summary>
    public void StageSelectButtonLeftOnClick()
    {
        if (m_StageSelectStage == StageSelectStage.Underground)
        {
            StageSelectTextCange(m_StageSelectStage);
        }
    }

    /// <summary>ステージ選択の右側のボタンが押された時</summary>
    public void StageSelectButtonRightOnClick()
    {
        if (m_StageSelectStage == StageSelectStage.Random)
        {
            StageSelectTextCange(m_StageSelectStage);
        }
    }

    void StageSelectTextCange(StageSelectStage stageSelectStage)
    {
        int StateNumber = 0;

        switch (stageSelectStage)
        {
            case StageSelectStage.Random : StateNumber = 1; break;
            case StageSelectStage.Underground : StateNumber = 0; break; 
        }

        for(int StageNumber = 0; StageNumber < m_SelectStageTextObjects.Length; StageNumber++)
        {
            if(StageNumber == StateNumber)
            {
                m_SelectStageTextObjects[StageNumber].SetActive(true);
            }
            else
            {
                m_SelectStageTextObjects[StageNumber].SetActive(false);
            }
        }

        switch (StateNumber)
        {
            case 0 : m_StageSelectStage = StageSelectStage.Random; break;
            case 1 : m_StageSelectStage = StageSelectStage.Underground; break;
        }
    }
}
