using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Thc6 : MonoBehaviour
{
    /// <summary>徘徊ルート</summary>
    GameObject[] m_PatrolPoints;

    /// <summary>キャラクターコントローラー</summary>
    CharacterController m_CharacterController;
    /// <summary>ナビメッシュエージェント</summary>
    NavMeshAgent m_NavMeshAgent;
    /// <summary>アニメーター</summary>
    Animator m_Animator;

    /// <summary>現在の徘徊ルート</summary>
    int m_CurrentPatrolPointIndex = -1;
    /// <summary>前回の徘徊ルートの番号を保存する変数</summary>
    Queue m_LastTimePatrolPointIndex = new Queue(15);

    /// <summary>重力加速度</summary>
    float m_Gravity = -9.81f;

    void Start() {
        // 徘徊ルートを取得
        m_PatrolPoints = GameObject.FindGameObjectsWithTag("thc6_PatrolPoint");
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

        // 徘徊ルートの番号を保存
        m_LastTimePatrolPointIndex.Enqueue(m_CurrentPatrolPointIndex);
        // 徘徊ルートのランダム
        m_CurrentPatrolPointIndex = Random.Range(0, m_PatrolPoints.Length);
        // 今まで保存していた徘徊ルートの番号と比較し、同じ番号がある場合
        while (m_LastTimePatrolPointIndex.Contains(m_CurrentPatrolPointIndex)) {
            // 再ランダム
            m_CurrentPatrolPointIndex = Random.Range(0, m_PatrolPoints.Length);
            // 今まで保存していた徘徊ルートの番号と比較し、同じ番号がない場合
            if (!m_LastTimePatrolPointIndex.Contains(m_CurrentPatrolPointIndex)) {
                // 無限ループから抜ける
                break;
            }
        }

        // 現在の徘徊ルートが現在設定されている徘徊ルートの番号以上だった場合
        if (m_CurrentPatrolPointIndex >= m_PatrolPoints.Length) {
            // 現在の徘徊ルートのリセット
            m_CurrentPatrolPointIndex = 0;
        }
        // 現在の徘徊ルートを反映
        m_NavMeshAgent.destination = m_PatrolPoints[m_CurrentPatrolPointIndex].transform.position;

        // 保存するキューの要素数が15以上になった場合
        if(m_LastTimePatrolPointIndex.Count >= 15) {
            // 古い要素を削除
            m_LastTimePatrolPointIndex.Dequeue();
        }
    }

    /// <summary>到着したか？</summary>
    /// <returns></returns>
    bool HasArrived() {
        return Vector3.Distance(m_NavMeshAgent.destination, transform.position) < 0.1f;
    }
}
