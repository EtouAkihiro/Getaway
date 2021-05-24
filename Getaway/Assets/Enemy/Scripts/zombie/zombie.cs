using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    // 他の敵
    [SerializeField, Header("Thc6")]
    GameObject m_Thc6Object;
    [SerializeField, Header("Creature")]
    GameObject m_CreatureObject;

    /// <summary>徘徊ルート</summary>
    GameObject[] m_PatrolPoints;

    /// <summary>キャラクターコントローラー</summary>
    CharacterController m_CharacterController;
    /// <summary>ナビメッシュエージェント</summary>
    NavMeshAgent m_NavMeshAgent;
    /// <summary>アニメーター</summary>
    Animator m_Animator;

    // 敵のスクリプト
    /// <summary>Thc6のスクリプト</summary>
    Thc6 m_Thc6;
    /// <summary>Creatureのスクリプト</summary>
    Creature m_Creature;

    /// <summary>現在の徘徊ルート</summary>
    int m_CurrentPatrolPointIndex = -1;
    /// <summary>前回の徘徊ルートの番号を保存する変数</summary>
    Queue m_LastTimePatrolPointIndex = new Queue(15);

    /// <summary>時間</summary>
    float m_Time = 0.0f;
    /// <summary>立ち上がるアニメーションの終了</summary>
    bool m_StandUpAnimatorEnd = false;

    /// <summary>Thc6のActiveの状態</summary>
    bool m_Thc6IsActive;
    /// <summary>CreatureのAciveの状態</summary>
    bool m_CreatureIsActive;

    /// <summary>移動量のアニメーションハッシュ</summary>
    int s_moveingHash = Animator.StringToHash("moving");

    void Start()
    {
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

    void Update()
    {
        // 徘徊ルートの位置に到着した場合
        // なおかつ、経過時間が2以上ある場合
        // なおかつ、立ち上がるアニメーションの終了している場合
        if (HasArrived() && m_Time >= 2 && m_StandUpAnimatorEnd)
        {
            // 次に向かう場所を決定
            SetNewPatrolPointToDestination();
            // 経過時間をリセット
            m_Time = 0.0f;
        }

        // 目的地についた場合
        if (HasArrived() && m_StandUpAnimatorEnd)
        {
            // 時間経過
            m_Time += Time.deltaTime;
        }
        // アニメーションを反映
        m_Animator.SetFloat(s_moveingHash, m_NavMeshAgent.velocity.sqrMagnitude);
    }

    /// <summary>次に向かう場所を決定</summary>
    void SetNewPatrolPointToDestination()
    {
        // 徘徊ルートの番号を指定
        m_CurrentPatrolPointIndex = RandomPatrolPointIndex(m_CurrentPatrolPointIndex);
        // 現在の徘徊ルートが現在設定されている徘徊ルートの番号以上だった場合
        if (m_CurrentPatrolPointIndex >= m_PatrolPoints.Length) {
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
        // 徘徊ルートのランダム
        int Result = Random.Range(0, m_PatrolPoints.Length);
        // 今まで保存していた徘徊ルートの番号と比較し、同じ番号がある場合
        while (m_LastTimePatrolPointIndex.Contains(Result))
        {
            // 再ランダム
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

    /// <summary>立ち上がるアニメーションの終了</summary>
    /// <returns></returns>
    bool isStandUpAnimatorEnd()
    {
        return m_StandUpAnimatorEnd = true;
    }

    /// <summary>現在の徘徊ルート番号</summary>
    public int CurrentPatrolPontIndex
    {
        get { return m_CurrentPatrolPointIndex; }
        set { m_CurrentPatrolPointIndex = value; }
    }
}
