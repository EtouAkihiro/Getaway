using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Creature : MonoBehaviour
{
    // 他の敵
    [SerializeField,Header("Th6")]
    GameObject m_Thc6Object;
    [SerializeField,Header("Zombie")]
    GameObject m_ZombieObject;

    /// <summary>徘徊ルート</summary>
    GameObject[] m_PatrolPoints;
    /// <summary>ナビメッシュエージェント</summary>
    NavMeshAgent m_NavMeshAgent;
    /// <summary>アニメーター</summary>
    Animator m_Animator;

    // 敵のスクリプト
    /// <summary>Thc6のスクリプト</summary>
    Thc6 m_Thc6;
    /// <summary>Zpmbieのスクリプト</summary>
    Zombie m_Zombie;

    /// <summary>現在の徘徊ルート</summary>
    int m_CurrentPatrolPointIndex = -1;
    /// <summary>前回の徘徊ルートの番号を保存する変数</summary>
    Queue m_LastTimePoatorlPointIndex = new Queue(15);

    /// <summary>Thc6の現在の徘徊ルート</summary>
    int m_Thc6CurrentPatrolPointIndex = -1;
    /// <summary>Zombieの現在の徘徊ルート</summary>
    int m_ZombieCurrentPatrolPointIndex = -1;

    /// <summary>時間</summary>
    float m_Time = 0.0f;

    /// <summary>Thc6のActiveの状態</summary>
    bool m_Thc6IsActive = false;
    /// <summary>ZombieのActiveの状態</summary>
    bool m_ZombieIsActive = false;

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
        // Thc6オブジェクトがある場合
        // Thc6スクリプトを取得
        // 現在のActive状態を取得
        if(m_Thc6Object != null)
        {
            m_Thc6IsActive = m_Thc6Object.activeSelf;
            m_Thc6 = m_Thc6Object.GetComponent<Thc6>();
        }

        // Zombieオブジェクトがある場合
        // Zombieスクリプトを取得
        if(m_ZombieObject != null)
        {
            m_ZombieIsActive = m_ZombieObject.activeSelf;
            m_Zombie = GetComponent<Zombie>();
        }
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
        m_CurrentPatrolPointIndex = RandomPoatrilPointIndex(m_CurrentPatrolPointIndex);
        // 現在の徘徊ルートが現在設定されている徘徊ルートの番号以上だった場合
        if(m_CurrentPatrolPointIndex >= m_PatrolPoints.Length)
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
    /// <param name="CurrentPotrolPointIndex">現在の徘徊ルートの番号</param>
    /// <returns></returns>
    int RandomPoatrilPointIndex(int CurrentPotrolPointIndex)
    {
        // 徘徊ルートの番号を保存
        m_LastTimePoatorlPointIndex.Enqueue(CurrentPotrolPointIndex);
        // 徘徊ルートの番号をランダムで指定
        int Result = Random.Range(0, m_PatrolPoints.Length);
        // Thc6のスクリプトを取得出来ている場合
        // Thc6の現在の徘徊ルート番号を取得
        if (m_Thc6 != null && m_Thc6IsActive) m_Thc6CurrentPatrolPointIndex = m_Thc6.CurrentPatrolPontIndex;
        // Zombieのスクリプトを取得出来ている場合
        // Zombieの現在の徘徊ルート番号を取得
        if (m_Zombie != null && m_ZombieIsActive) m_ZombieCurrentPatrolPointIndex = m_Zombie.CurrentPatrolPontIndex;

        // 今まで保存してきた徘徊ルートの番号と比較し、同じ番号がある場合
        // また、Thc6の現在の徘徊ルートの番号が同じ番号である場合
        // また、Zombieの現在の徘徊ルートの番号が同じ番号である場合
        // どれかの条件がヒットしたら、無限ループする。
        while (m_LastTimePoatorlPointIndex.Contains(Result) && 
               m_Thc6CurrentPatrolPointIndex == Result &&
               m_ZombieCurrentPatrolPointIndex == Result)
        {
            // 再ランダムを行う
            Result = Random.Range(0, m_PatrolPoints.Length);
            // 再ランダムの結果、今まで保存してきた徘徊ルートの番号がない場合
            if (!m_LastTimePoatorlPointIndex.Contains(Result) &&
                Result != m_Thc6CurrentPatrolPointIndex && 
                Result != m_ZombieCurrentPatrolPointIndex)
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

    /// <summary>現在の徘徊ルート番号</summary>
    public int CurrentPatrolPontIndex
    {
        get { return m_CurrentPatrolPointIndex; }
        set { m_CurrentPatrolPointIndex = value; }
    }
}
