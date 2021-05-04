using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Photon.Pun;
using Photon.Realtime;
using Photon.Chat;

/// <summary>ルームシーン内のボタンを管理するクラス</summary>
public class RoomButtonController : MonoBehaviourPunCallbacks, IPunObservable
{
    /// <summary>選択されるステージのテキストのオブジェクト</summary>
    public GameObject[] m_SelectStageTextObjects;
    /// <summary>ステージの画像のオブジェクト</summary>
    public GameObject[] m_StageImageObjects;

    /// <summary>ステージレベル</summary>
    int m_StageSceneNamesNumber = -1;
    /// <summary>受信用のステージレベル</summary>
    int m_ShareStageSceneNamesNumber = -1;
    /// <summary>ゲーム開始するかのフラグ</summary>
    bool m_GamePlayFrag = false;

    /// <summary>選択できるステージの状態</summary>
    enum StageSelectState
    {
        /// <summary>ランダム</summary>
        Random = 0,
        /// <summary>地下</summary>
        Underground = 1,
    }

    /// <summary>選択されているステージの状態</summary>
    StageSelectState m_StageSelectState;
    /// <summary>送信されたステージの状態</summary>
    StageSelectState m_ShareStageSelectState;
    /// <summary>現在存在するステージのシーン名</summary>
    List<string> m_StageSceneNameList = new List<string>();

    void Start()
    {
        // 最初はRandomを選択した状態にする。
        m_StageSelectState = StageSelectState.Random;
        // 初期状態をRandomに設定
        m_ShareStageSelectState = m_StageSelectState;
        // 地下のテキストオブジェクトを非表示にする
        m_SelectStageTextObjects[1].SetActive(false);
        // ステージのイメージ画像を表示
        StageImageDisPlay();
        // 現在のステージのシーン名をリストに格納
        m_StageSceneNameList.Add("UndergroundGamePlayScene");
    }

    void Update()
    {
        if (!PhotonNetwork.IsMasterClient && m_StageSelectState != m_ShareStageSelectState)
        {
            // 現在のステージの状態を更新
            m_StageSelectState = m_ShareStageSelectState;
            // そして、ステージセレクトを遷移する
            StageSelectTextCange(m_StageSelectState);
        }

        // 参加者用シーン遷移
        if(!PhotonNetwork.IsMasterClient && m_GamePlayFrag)
        {
        }
    }

    /// <summary>ステージ選択の左側のボタンが押された時</summary>
    public void StageSelectButtonLeftOnClick()
    {
        // 現在のステージ選択の状態が地下が選択された場合
        // 状態の遷移を行う。
        if (m_StageSelectState == StageSelectState.Underground)
        {
            StageSelectTextCange(m_StageSelectState);
        }
    }

    /// <summary>ステージ選択の右側のボタンが押された時</summary>
    public void StageSelectButtonRightOnClick()
    {
        // 現在のステージ選択の状態がランダムが選択された場合
        // 状態の遷移を行う。
        if (m_StageSelectState == StageSelectState.Random)
        {
            StageSelectTextCange(m_StageSelectState);
        }
    }

    /// <summary>ゲームプレイボタンが押された時</summary>
    public void GamePlayOnClick()
    {
        // 現在の選択の状態が、ランダムだった場合
        if (m_StageSelectState == StageSelectState.Random)
        {
            // ランダムでステージのナンバーを取得
            int RandomStageNumber = Random.Range(0, m_StageSceneNameList.Count);
            // ステージナンバーを保存
            m_StageSceneNamesNumber = RandomStageNumber;
            // そのシーン遷移
            Fade.Instance.FadeOut(m_StageSceneNameList[m_StageSceneNamesNumber]);
        }
        // 現在の選択状態が、選択されている場合
        else
        {
            switch (m_StageSelectState)
            {
                case StageSelectState.Underground : Fade.Instance.FadeOut(m_StageSceneNameList[0]); break;
            }
        }
    }

    /// <summary>ルーム退出</summary>
    public void RoomExitOnClick()
    {
        // ルーム退出
        PhotonManager.Instance.LeaveRoom();
        // ルーム退出をtrue
        GameController.Instance.RoomExit = true;
        // タイトルシーンに遷移
        Fade.Instance.FadeOut("TitleScene");
    }

    /// <summary>ステージ選択のテキストの状態遷移</summary>
    /// <param name="stageSelectStage">ステージ選択の状態</param>
    void StageSelectTextCange(StageSelectState stageSelectStage)
    {
        // ステージ選択の状態を値で表すための変数
        int StateNumber = 0;

        // 次のステージ選択の値を取得
        switch (stageSelectStage)
        {
            case StageSelectState.Random : StateNumber = 1; break;
            case StageSelectState.Underground : StateNumber = 0; break; 
        }

        // 表示されるかどうか
        for(int StageNumber = 0; StageNumber < m_SelectStageTextObjects.Length; StageNumber++)
        {
            // ルームマスターだった場合
            if (PhotonNetwork.IsMasterClient)
            {
                // ステージ選択の値とステージの値が等しい場合
                // そのテキストを表示する。
                // それ以外は、非表示にする。
                if (StageNumber == StateNumber)
                {
                    m_SelectStageTextObjects[StageNumber].SetActive(true);
                }
                else
                {
                    m_SelectStageTextObjects[StageNumber].SetActive(false);
                }
            }
            // 参加者だった場合
            else
            {
                // ステージ選択の値とステージの値が異なる場合
                // そのテキストを表示する。
                // それ以外は、非表示にする。
                if (StageNumber != StateNumber)
                {
                    m_SelectStageTextObjects[StageNumber].SetActive(true);
                }
                else
                {
                    m_SelectStageTextObjects[StageNumber].SetActive(false);
                }
            }
        }

        // ルームマスターだった場合
        if (PhotonNetwork.IsMasterClient)
        {
            // 状態の値で状態遷移
            switch (StateNumber)
            {
                case 0: m_StageSelectState = StageSelectState.Random; break;
                case 1: m_StageSelectState = StageSelectState.Underground; break;
            }
        }

        // ステージのイメージ画像を表示
        StageImageDisPlay();
    }

    /// <summary>ステージのイメージ画像を表示</summary>
    void StageImageDisPlay()
    {
        // ステージナンバー
        int StageNumber = (int)m_StageSelectState;

        for (int StageImageNumber = 0; StageImageNumber < m_StageImageObjects.Length; StageImageNumber++)
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

    #region IPubObservable implementation

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // 送信
        if (stream.IsWriting)
        {
            stream.SendNext((int)m_StageSelectState);
            print("送信");
        }
        // 受信
        else
        {
            this.m_ShareStageSelectState = (StageSelectState)(int)stream.PeekNext();
            print("受信");
        }
    }

    #endregion
}
