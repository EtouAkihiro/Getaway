using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Problem : MonoBehaviour
{
    /// <summary>問題文</summary>
    string m_Problem;
    /// <summary>答え</summary>
    string m_Answer;
    /// <summary>間違えの答え</summary>
    string[] m_FalseAnswers = new string[3];

    void Start()
    {
        // 問題を取得
        m_Problem = ProblemController.Instance.GetProblem();
        // 答えを取得
        m_Answer = ProblemController.Instance.GetAnswer(m_Problem);
    }

    /// <summary>接触している間のあたり</summary>
    void OnCollisionStay(Collision collision)
    {
    }
}
