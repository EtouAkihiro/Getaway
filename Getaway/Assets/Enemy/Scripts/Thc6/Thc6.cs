﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Thc6 : MonoBehaviour
{
    /// <summary>徘徊ルート</summary>
    GameObject[] m_PatrolPoints;
    /// <summary>ナビメッシュエージェント</summary>
    NavMeshAgent m_NavMeshAgent;
    /// <summary>アニメーター</summary>
    Animator m_Animator;

    /// <summary>現在の徘徊ルート</summary>
    int m_CurrentPatrolPointIndex = -1;
    /// <summary>前回の徘徊ルートの番号を保存する変数</summary>
    Queue m_LastTimePatrolPointIndex = new Queue(15);

    /// <summary>時間</summary>
    float m_Time = 0.0f;

    /// <summary>移動量のアニメーションハッシュ</summary>
    int s_MoveingHash = Animator.StringToHash("moving");
    /// <summary>バトルのアニメーションハッシュ</summary>
    int s_BattleHash = Animator.StringToHash("battle");

    void Start()
    {
        // 徘徊ルートを取得
        m_PatrolPoints = GameObject.FindGameObjectsWithTag("thc6_PatrolPoint");
        // ナビメッシュエージェントの取得
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        // アニメーターを取得
        m_Animator = GetComponent<Animator>();
        // ランダムで徘徊ルートの開始
        m_CurrentPatrolPointIndex = Random.Range(0, m_PatrolPoints.Length);
    }

    void Update()
    {
        // 到着した場合
        if (HasArrived() && m_Time >= 2)
        {
            // 次に向かう場所を指定
            SetNewPatrolPointToDestination();
            // 経過時間をリセット
            m_Time = 0.0f;
        }
        // 目的地についた場合
        else if (HasArrived()) 
        {
            // 時間経過
            m_Time += Time.deltaTime;
        }

        // アニメーションを反映
        m_Animator.SetInteger(s_MoveingHash, (int)m_NavMeshAgent.velocity.sqrMagnitude);
    }

    /// <summary>次に向かう場所を決定</summary>
    void SetNewPatrolPointToDestination()
    {
        // 徘徊ルートの番号を指定
        m_CurrentPatrolPointIndex = RandomPatrolPointIndex(m_CurrentPatrolPointIndex); 
        // 現在の徘徊ルートが現在設定されている徘徊ルートの番号以上だった場合
        if (m_CurrentPatrolPointIndex >= m_PatrolPoints.Length)
        {
            // 現在の徘徊ルートのリセット
            m_CurrentPatrolPointIndex = 0;
        }
        // 現在の徘徊ルートを反映
        m_NavMeshAgent.destination = m_PatrolPoints[m_CurrentPatrolPointIndex].transform.position;
    }

    /// <summary>到着したか？</summary>
    /// <returns></returns>
    bool HasArrived()
    {
        return Vector3.Distance(m_NavMeshAgent.destination, transform.position) < 0.5f;
    }

    /// <summary>ランダムで徘徊ルートの番号を指定</summary>
    /// <param name="CurrentPatrolPointIndex">現在の徘徊ルートの番号</param>
    /// <returns></returns>
    int RandomPatrolPointIndex(int CurrentPatrolPointIndex)
    {
        // 徘徊ルートの番号を保存
        m_LastTimePatrolPointIndex.Enqueue(CurrentPatrolPointIndex);
        // 徘徊ルートの番号をランダムで指定
        int Result = Random.Range(0, m_PatrolPoints.Length);
        // 今まで保存していた徘徊ルートの番号と比較し、同じ番号がある場合
        while (m_LastTimePatrolPointIndex.Contains(Result))
        {
            // 再ランダムを行う
            Result = Random.Range(0, m_PatrolPoints.Length);
            // 今まで保存していた徘徊ルートの番号と比較し、同じ番号がない場合
            if (!m_LastTimePatrolPointIndex.Contains(Result))
            {
                // 無限ループから抜ける
                break;
            }
        }

        // 保存するキューの要素数が15以上になった場合
        if (m_LastTimePatrolPointIndex.Count >= 15)
        {
            // 古い要素を削除
            m_LastTimePatrolPointIndex.Dequeue();
        }

        // 結果を返す
        return Result;
    }

    /// <summary>現在の徘徊ルート番号</summary>
    public int CurrentPatrolPontIndex
    {
        get { return m_CurrentPatrolPointIndex; }
        set { m_CurrentPatrolPointIndex = value; }
    }
}
