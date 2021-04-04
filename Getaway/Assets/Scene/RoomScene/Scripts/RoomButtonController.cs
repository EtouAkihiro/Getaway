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
    }

    /// <summary>ステージ選択の左側のボタンが押された時</summary>
    public void StageSelectButtonLeftOnClick()
    {
        switch (m_StageSelectStage)
        {
            case StageSelectStage.Random : StageSelectMoveLeft(m_SelectStageTextObjects[1], m_SelectStageTextObjects[0]); break;
            case StageSelectStage.Underground : StageSelectMoveLeft(m_SelectStageTextObjects[0], m_SelectStageTextObjects[1]); break;
        }
    }

    void StageSelectMoveLeft(GameObject NextSelectStageText, GameObject CurrentSelectStageText)
    {
        float LeftEndPositionX = 150.0f;

        if (!NextSelectStageText.activeSelf)
        {
            NextSelectStageText.SetActive(true);
        }

        // 非表示
        TextDontDisPlay(CurrentSelectStageText, -LeftEndPositionX);
        // 表示
        TextDisPlay(NextSelectStageText, LeftEndPositionX);

        if(m_DisPlayFrag && m_DontDisPlayFrag)
        {
            switch (m_StageSelectStage)
            {
                case StageSelectStage.Random : m_StageSelectStage = StageSelectStage.Underground; break;
                case StageSelectStage.Underground : m_StageSelectStage = StageSelectStage.Random; break;
            }

            // フラグを初期化
            m_DisPlayFrag = false;
            m_DontDisPlayFrag = false;
        }
    }

    void StageSelectMoveRight(GameObject NextSelectStageText, GameObject CurrentSelectStageText, int StageSelectStage)
    {
        // 表示

        // 非表示
    }

    /// <summary>テキストの表示</summary>
    /// <param name="DisPlayObject">表示するオブジェクト</param>
    /// <param name="EndPositionX">最終的なX座標の位置</param>
    void TextDisPlay(GameObject DisPlayObject, float EndPositionX)
    {
        bool MovedFrag = false;
        bool TextColorChangedFrag = false;

        // 表示位置に移動
        Transform DisPlayObjectTr = DisPlayObject.transform;
        DisPlayObjectTr.DOMoveX(EndPositionX, 1.0f)
            .From()
            .SetEase(Ease.OutQuart)
            .OnComplete(() => {
                // 表示位置に移動したら、フラグをtrue
                MovedFrag = true;
            });

        // 徐々に表示
        Text DisPlayObjectTx = DisPlayObject.GetComponent<Text>();
        DisPlayObjectTx.DOColor(new Color(1.0f, 1.0f, 1.0f, 1.0f), 1.0f)
            .From()
            .SetEase(Ease.OutQuart)
            .OnComplete(() => {
                // 表示されたら、フラグをtrue
                TextColorChangedFrag = true;
            });

        // 表示位置に移動し、尚且つ、表示された状態なら
        if(MovedFrag && TextColorChangedFrag)
        {
            // 表示された状態にする。
            m_DisPlayFrag = true;
        }
    }

    /// <summary>テキストの非表示</summary>
    /// <param name="DontDisPlayObject">非表示にするオブジェクト</param>
    /// <param name="EndPositionX">最終的なX座標の位置</param>
    void TextDontDisPlay(GameObject DontDisPlayObject, float EndPositionX)
    {
        bool MovedFrag = false;
        bool TextColorChangedFrag = false;

        // 非表示位置に移動
        Transform DontDisPlayTr = DontDisPlayObject.transform;

        // 徐々に非表示
        Text DontDisPlayTx = DontDisPlayObject.GetComponent<Text>();
        DontDisPlayTx.DOColor(new Color(1.0f, 1.0f, 1.0f, 0.0f), 1.0f)
            .From()
            .SetEase(Ease.OutQuart)
            .OnComplete(() => {
                TextColorChangedFrag = true;
            });

        if(MovedFrag && TextColorChangedFrag)
        {
        }
    }
}
