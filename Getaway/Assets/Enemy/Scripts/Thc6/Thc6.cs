using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Thc6 : MonoBehaviour
{
    /// <summary>徘徊ルート</summary>
    public Transform[] m_PatrolPoints;

    /// <summary>キャラクターコントローラー</summary>
    CharacterController m_CharacterController;
    /// <summary>ナビメッシュエージェント</summary>
    NavMeshAgent m_NavMeshAgent;
    /// <summary>アニメーター</summary>
    Animator m_Animator;

    /// <summary>現在の徘徊ルートのカウンター</summary>
    int m_CurrentPatrolPointIndex = -1;

    /// <summary>重力加速度</summary>
    float m_Gravity = -9.81f;

    void Start() {
        // キャラクターコントローラーの取得
        m_CharacterController = GetComponent<CharacterController>();
        // ナビメッシュエージェントの取得
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        // アニメーターを取得
        m_Animator = GetComponent<Animator>();
        // ランダムで徘徊ルートの開始
        m_CurrentPatrolPointIndex = Random.Range(0, m_PatrolPoints.Length);
    }

    void Update() {

        // 到着した場合
        if (HasArrived())
        {
            // 次に向かう場所を決定
            SetNewPatrolPointToDestination();
        }
    }

    /// <summary>次に向かう場所を決定</summary>
    void SetNewPatrolPointToDestination() {
        // 徘徊ルートのカウンターアップ
        m_CurrentPatrolPointIndex = Random.Range(0, m_PatrolPoints.Length);
        // 現在の徘徊ルートが現在設定されている徘徊ルートの番号以上だった場合
        if(m_CurrentPatrolPointIndex >= m_PatrolPoints.Length) {
            // 現在の徘徊ルートのリセット
            m_CurrentPatrolPointIndex = 0;
        }
        // 現在の徘徊ルートを反映
        m_NavMeshAgent.destination = m_PatrolPoints[m_CurrentPatrolPointIndex].position;
    }

    /// <summary>到着したか？</summary>
    /// <returns></returns>
    bool HasArrived() {
        return Vector3.Distance(m_NavMeshAgent.destination, transform.position) < 0.1f;
    }
}
