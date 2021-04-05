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
        /// <summary>ランダム</summary>
        Random,
        /// <summary>地下</summary>
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
        // 現在のステージ選択の状態が地下が選択された場合
        // 状態の遷移を行う。
        if (m_StageSelectStage == StageSelectStage.Underground)
        {
            StageSelectTextCange(m_StageSelectStage);
        }
    }

    /// <summary>ステージ選択の右側のボタンが押された時</summary>
    public void StageSelectButtonRightOnClick()
    {
        // 現在のステージ選択の状態がランダムが選択された場合
        // 状態の遷移を行う。
        if (m_StageSelectStage == StageSelectStage.Random)
        {
            StageSelectTextCange(m_StageSelectStage);
        }
    }

    /// <summary>ステージ選択のテキストの状態遷移</summary>
    /// <param name="stageSelectStage">ステージ選択の状態</param>
    void StageSelectTextCange(StageSelectStage stageSelectStage)
    {
        // ステージ選択の状態を値で表すための変数
        int StateNumber = 0;

        // 次のステージ選択の値を取得
        switch (stageSelectStage)
        {
            case StageSelectStage.Random : StateNumber = 1; break;
            case StageSelectStage.Underground : StateNumber = 0; break; 
        }

        // 表示されるかどうか
        for(int StageNumber = 0; StageNumber < m_SelectStageTextObjects.Length; StageNumber++)
        {
            // ステージ選択の値とステージの値が等しい場合
            // そのテキストを表示する。
            // それ以外は、非表示にする。
            if(StageNumber == StateNumber)
            {
                m_SelectStageTextObjects[StageNumber].SetActive(true);
            }
            else
            {
                m_SelectStageTextObjects[StageNumber].SetActive(false);
            }
        }

        // 状態の値で状態遷移
        switch (StateNumber)
        {
            case 0 : m_StageSelectStage = StageSelectStage.Random; break;
            case 1 : m_StageSelectStage = StageSelectStage.Underground; break;
        }
    }
}
