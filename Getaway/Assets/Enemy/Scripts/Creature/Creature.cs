using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Creature : MonoBehaviour
{
    /// <summary>徘徊ルート</summary>
    GameObject[] m_PatrolPoints;
    /// <summary>ナビメッシュエージェント</summary>
    NavMeshAgent m_NavMeshAgent;
    /// <summary>アニメーター</summary>
    Animator m_Animator;

    /// <summary>現在の徘徊ルート</summary>
    int m_CurrentPatrolPontIndex = -1;
    /// <summary>前回の徘徊ルートの番号を保存する変数</summary>
    Queue m_LastTimePoatorlPointIndex = new Queue(15);

    /// <summary>時間</summary>
    float m_Time = 0.0f;

    void Start()
    {
        // 徘徊ルートを取得
        m_PatrolPoints = GameObject.FindGameObjectsWithTag("thc6_PatrolPoint");
        // ナビメッシュエージェントの取得
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        // アニメーターを取得
        m_Animator = GetComponent<Animator>();
        // ランダムで徘徊ルートの開始
        m_CurrentPatrolPontIndex = Random.Range(0, m_PatrolPoints.Length);
    }

    void Update()
    {
        // 目的地に到着して
        // 経過時間が2秒以上だった場合
        if (HasArrived() && m_Time >= 2)
        {
            // 次に向かう場所を指定
            SetNewPatrolPointToDestination();
            // 経過時間をリセット
            m_Time = 0.0f;
        }
        // 目的地に到着した場合
        else if (HasArrived())
        {
            // 時間経過
            m_Time += Time.deltaTime;
        }
    }

    void SetNewPatrolPointToDestination()
    {
        // 徘徊ルートの番号を指定
        m_CurrentPatrolPontIndex = RandomPoatrilPointIndex(m_CurrentPatrolPontIndex);
        // 現在の徘徊ルートが現在設定されている徘徊ルートの番号以上だった場合
        if(m_CurrentPatrolPontIndex >= m_PatrolPoints.Length)
        {
            // 現在の徘徊ルートのリセット
            m_CurrentPatrolPontIndex = 0;
        }
        // 現在の徘徊ルートを反映
        m_NavMeshAgent.destination = m_PatrolPoints[m_CurrentPatrolPontIndex].transform.position;
    }

    /// <summary>到着したか？</summary>
    /// <returns></returns>
    bool HasArrived()
    {
        return Vector3.Distance(m_NavMeshAgent.destination, transform.position) < 0.5f;
    }

    /// <summary>ランダムで徘徊ルートの番号を指定</summary>
    /// <param name="CurrentPotrolPointIndex">現在の徘徊ルートの番号</param>
    /// <returns></returns>
    int RandomPoatrilPointIndex(int CurrentPotrolPointIndex)
    {
        // 徘徊ルートの番号を保存
        m_LastTimePoatorlPointIndex.Enqueue(CurrentPotrolPointIndex);
        // 徘徊ルートの番号をランダムで指定
        int Result = Random.Range(0, m_PatrolPoints.Length);
        // 今まで保存してきた徘徊ルートの番号と比較し、同じ番号がある場合
        // 無限ループにする。
        while (m_LastTimePoatorlPointIndex.Contains(Result))
        {
            // 再ランダムを行う
            Result = Random.Range(0, m_PatrolPoints.Length);
            // 再ランダムの結果、今まで保存してきた徘徊ルートの番号がない場合
            if (!m_LastTimePoatorlPointIndex.Contains(Result))
            {
                // 無限ループから抜ける
                break;
            }
        }

        // 保存した徘徊ルートの番号の個数が15個以上ある場合
        if(m_LastTimePoatorlPointIndex.Count >= 15)
        {
            // 古い要素を削除
            m_LastTimePoatorlPointIndex.Dequeue();
        }

        // ランダム結果を返す
        return Result;
    }
}
