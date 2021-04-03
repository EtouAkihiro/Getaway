using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>タイトル画面のEventTiggerを管理するクラス</summary>
public class TitleEventTrigger : MonoBehaviour
{
    /// <summary>プレイヤー名の説明文</summary>
    public GameObject m_SelectPlayerNameMessageObject;
    /// <summary>マッチングの説明文</summary>
    public GameObject m_SelectMatchingMessageObject;
    /// <summary>ルーム作成の説明文</summary>
    public GameObject m_SelectRoomCreationMessageObject;
    /// <summary>ルームパスワードの説明文</summary>
    public GameObject m_SelectRoomPasswordMessageObject;
    /// <summary>タイトルに戻るの説明文</summary>
    public GameObject m_SelectBackToTitleMessageObject;

    /// <summary>マウスが名前入力が選択されている時、プレイヤー名の説明文を表示</summary>
    public void MouseSelectMessageEvent_DisPlay_PlayerName()
    {
        MessageDisPlay(m_SelectPlayerNameMessageObject);
    }

    /// <summary>マウスがマッチングが選択されている時、マッチングの説明文を表示</summary>
    public void MouseSelectMessageEvent_DisPlay_Matching()
    {
        MessageDisPlay(m_SelectMatchingMessageObject);
    }

    /// <summary>マウスがルーム作成が選択されている時、ルーム作成の説明文を表示</summary>
    public void MouseSelectMessageEvent_DisPlay_RoomCreation()
    {
        MessageDisPlay(m_SelectRoomCreationMessageObject);
    }

    /// <summary>マウスがルームパスワードが選択されている時、ルームパスワードの説明文を表示</summary>
    public void MouseSelectMessageEvent_DisPlay_RoomPaswwoed()
    {
        MessageDisPlay(m_SelectRoomPasswordMessageObject);
    }

    /// <summary>マウスがタイトルに戻るが選択されている時、タイトルに戻るの説明文を表示</summary>
    public void MouseSelectMessageEvent_DisPlay_BackToTitle()
    {
        MessageDisPlay(m_SelectBackToTitleMessageObject);
    }



    /// <summary>マウスが名前入力が選択から外れたら、プレイヤー名の説明文を非表示</summary>
    public void MouseSelectMessageEvent_DontDisPlay_PlayerName()
    {
        MessageDontDisPlay(m_SelectPlayerNameMessageObject);
    }

    /// <summary>マウスがマッチングが選択から外れたら、マッチングの説明文を非表示</summary>
    public void MouseSelectMessageEvent_DontDisPlay_Matching()
    {
        MessageDontDisPlay(m_SelectMatchingMessageObject);
    }

    /// <summary>マウスがルーム作成が選択から外れたら、ルーム作成の説明文を非表示</summary>
    public void MouseSelectMessageEvent_DontDisPlay_RoomCreation()
    {
        MessageDontDisPlay(m_SelectRoomCreationMessageObject);
    }

    /// <summary>マウスがルームパスワードが選択から外れたら、ルームパスワードの説明文を非表示</summary>
    public void MouseSelectMessageEvent_DontDisPlay_RoomPassword()
    {
        MessageDontDisPlay(m_SelectRoomPasswordMessageObject);
    }

    /// <summary>マウスがタイトルに戻るが選択から外れたら、タイトルに戻るの説明文を非表示</summary>
    public void MouseSelectMessageEvent_DontDisPlay_BackToTitle()
    {
        MessageDontDisPlay(m_SelectBackToTitleMessageObject);
    }

    /// <summary>説明文を表示</summary>
    /// <param name="SelectMessageObject">表示したいオブジェクト</param>
    void MessageDisPlay(GameObject SelectMessageObject)
    {
        // 表示したいオブジェクトが非表示になっていた場合、表示する。
        if (!SelectMessageObject.activeSelf)
        {
            SelectMessageObject.SetActive(true);
        }
    }

    /// <summary>説明文を非表示</summary>
    /// <param name="SelectMeeageObject">非表示にしたいオブジェクト</param>
    void MessageDontDisPlay(GameObject SelectMeeageObject)
    {
        // 非表示にしたいオブジェクトが表示になっていた場合、非表示にする。
        if (SelectMeeageObject.activeSelf)
        {
            SelectMeeageObject.SetActive(false);
        }
    }
}
