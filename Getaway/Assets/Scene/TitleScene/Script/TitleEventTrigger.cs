using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>タイトル画面のEventTiggerを管理するクラス</summary>
public class TitleEventTrigger : MonoBehaviour
{
/// 表示 //////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>マウスが名前入力が選択されている時、プレイヤー名の説明文を表示</summary>
    public void MouseSelectMessgeEvent_DisPlay_PlayerName()
    {
    }

    /// <summary>マウスがマッチングが選択されている時、マッチングの説明文を表示</summary>
    public void MouseSelectMessgeEvent_DisPlay_Matching()
    {
    }

    /// <summary>マウスがルーム作成が選択されている時、ルーム作成の説明文を表示</summary>
    public void MouseSelectMessageEvent_DisPlay_RoomCreation()
    {
    }

    /// <summary>マウスがルームパスワードが選択されている時、ルームパスワードの説明文を表示</summary>
    public void MouseSelectMessageEvent_DisPlay_RoomPaswwoed()
    {
    }

    /// <summary>マウスがタイトルに戻るが選択されている時、タイトルに戻るの説明文を表示</summary>
    public void MouseSelectMessageEvent_DisPlay_BackToTitle()
    {
    }


/// 非表示 ////////////////////////////////////////////////////////////////////////////////////////////


    /// <summary>マウスが名前入力が選択から外れたら、プレイヤー名の説明文を非表示</summary>
    public void MouseSelectMessageEvent_DontDisPlay_PlayerName()
    {
    }

    /// <summary>マウスがマッチングが選択から外れたら、マッチングの説明文を非表示</summary>
    public void MouseSelectMessageEvent_DontDisPlay_Matching()
    {
    }

    /// <summary>マウスがルーム作成が選択から外れたら、ルーム作成の説明文を非表示</summary>
    public void MouseSelectMessageEvent_DontDisPlay_RoomCreation()
    {
    }

    /// <summary>マウスがルームパスワードが選択から外れたら、ルームパスワードの説明文を非表示</summary>
    public void MouseSelectMessageEvent_DontDisPlay_RoomPassword()
    {
    }

    /// <summary>マウスがタイトルに戻るが選択から外れたら、タイトルに戻るの説明文を非表示</summary>
    public void MouseSelectMessageEvent_DontDisPlay_BackToTitle()
    {
    }
}
