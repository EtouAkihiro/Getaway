using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Photon.Pun;
using Photon.Realtime;

/// <summary>ルームシーン内のボタンを管理するクラス</summary>
public class RoomButtonController : MonoBehaviour
{
    /// <summary>選択されるステージのテキストのオブジェクト</summary>
    public GameObject[] m_SelectStageTextObjects;
    /// <summary>ステージの画像のオブジェクト</summary>
    public GameObject[] m_StageImageObjects;

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
    /// <summary>現在存在するステージのシーン名</summary>
    List<string> m_StageSceneNameList = new List<string>();

    void Start()
    {
        // 最初はRandomを選択した状態にする。
        m_StageSelectStage = StageSelectStage.Random;
        // 地下のテキストオブジェクトを非表示にする
        m_SelectStageTextObjects[1].SetActive(false);
        // ステージのイメージ画像を表示
        StageImageDisPlay();
        // 現在のステージのシーン名をリストに格納
        m_StageSceneNameList.Add("UndergroundGamePlayScene");
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

    /// <summary>ゲームプレイボタンが押された時</summary>
    public void GamePlayOnClick()
    {
        // 現在の選択の状態が、ランダムだった場合
        if (m_StageSelectStage == StageSelectStage.Random)
        {
            // ランダムでステージのナンバーを取得
            int RandomStageNumber = Random.Range(0, 0);

            int StageLevel = -1;
            switch (RandomStageNumber)
            {
                case 0 : StageLevel = 2; break;
            }

            if (StageLevel == -1) return;

            // ランダムで決まった値をリストの番号に指定し、シーン遷移する。
            Fade.Instance.FadeOut(StageLevel);
        }
        else
        {
            switch (m_StageSelectStage)
            {
                case StageSelectStage.Underground : Fade.Instance.FadeOut(m_StageSceneNameList[0]); break;
            }
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

        // ステージのイメージ画像を表示
        StageImageDisPlay();
    }

    /// <summary>ステージのイメージ画像を表示</summary>
    void StageImageDisPlay()
    {
        // ステージナンバー
        int StageNumber = 0;

        switch (m_StageSelectStage)
        {
            case StageSelectStage.Random : StageNumber = 0; break;
            case StageSelectStage.Underground : StageNumber = 1; break;
        }

        for(int StageImageNumber = 0; StageImageNumber < m_StageImageObjects.Length; StageImageNumber++)
        {
            if (StageImageNumber == StageNumber)
            {
                m_StageImageObjects[StageImageNumber].SetActive(true);
            }
            else
            {
                m_StageImageObjects[StageImageNumber].SetActive(false);
            }
        }
    }
}
