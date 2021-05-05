using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>問題を管理するクラス</summary>
public class ProblemController : SingletonMOnoBehaviour<ProblemController>
{
    /// <summary>問題のオブジェクト</summary>
    GameObject[] m_ProblemsObjects;

    /// <summary>問題集</summary>
    string[] m_Problems;
    /// <summary>答え集</summary>
    string[] m_Answers;

    /// <summary>問題を割り当てを保存</summary>
    Queue m_ProblemsAllocationSave;

    void Start()
    {
        // 問題のオブジェクトを取得
        m_ProblemsObjects = GameObject.FindGameObjectsWithTag("Problems");
        // 問題を割り当て保存容量を設定
        m_ProblemsAllocationSave = new Queue(m_ProblemsObjects.Length);
        
    }

    /// <summary>問題を返す</summary>
    /// <returns></returns>
    string GetProblem()
    {
        // ランダムで問題の番号を取得
        int ProblemRandomNumber = Random.Range(0, m_Problems.Length);
        // もし、問題の番号が使われている場合
        while (m_ProblemsAllocationSave.Contains(ProblemRandomNumber))
        {
            // 再ランダムで問題の番号を取得
            ProblemRandomNumber = Random.Range(0, m_Problems.Length);
            // もし、再ランダムの結果番号が使われていない場合
            // 無限ルームから抜ける
            if (!m_ProblemsAllocationSave.Contains(ProblemRandomNumber))
            {
                break;
            }
        }

        // 問題の番号を保存する
        m_ProblemsAllocationSave.Enqueue(ProblemRandomNumber);
        // 問題を取得
        string Problem = m_Problems[ProblemRandomNumber];
        // 問題を返す
        return Problem;
    }

    /// <summary>答えを返す</summary>
    /// <param name="Problem">問題文</param>
    /// <returns></returns>
    string GetAnswer(string Problem)
    {
        // 問題の番号
        int ProblemNumver = -1;
        for(int i = 0; i < m_Problems.Length; i++)
        {
            if(Problem == m_Problems[i])
            {
                // 番号を保存する。
                ProblemNumver = i;
                // for文を抜ける。
                break;
            }
        }

        // 答えを取得
        string Answer = m_Answers[ProblemNumver];
        // 答えを返す
        return Answer;
    }
}
