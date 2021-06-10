using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField, Header("左目")]
    GameObject m_EveLeftObject;
    [SerializeField, Header("右目")]
    GameObject m_EveRightObject;

    /// <summary>左目のスキンメッシュのフィルター</summary>
    SkinnedMeshRenderer m_EveLeftSkinnedMeshRenderer;
    /// <summary>右目のスキンメッシュのフィルター</summary>
    SkinnedMeshRenderer m_EveRightSkinnedMeshRenderer;

    /// <summary>カメラのZ座標の補正値</summary>
    const float m_CameraPosZCorrection = 0.1f;

    void Start()
    {
        // 左目のスキンメッシュを取得
        m_EveLeftSkinnedMeshRenderer = m_EveLeftObject.GetComponent<SkinnedMeshRenderer>();
        // 右目のスキンメッシュを取得
        m_EveRightSkinnedMeshRenderer = m_EveRightObject.GetComponent<SkinnedMeshRenderer>();
    }

    void Update()
    {
        // カメラ位置更新
        CameraPosUpdate();
    }

    /// <summary>カメラ位置更新</summary>
    void CameraPosUpdate()
    {
        // 左目の位置を取得
        Vector3 EveLiftPosition = m_EveLeftSkinnedMeshRenderer.bounds.center;
        // 右目の位置を取得
        Vector3 EveRightPosition = m_EveRightSkinnedMeshRenderer.bounds.center;
        // 左右の目の中心点を算出
        Vector3 CenterPoint = Vector3.Lerp(EveLiftPosition, EveRightPosition, 0.5f);
        // カメラの位置を補正
        CenterPoint.z = CenterPoint.z + m_CameraPosZCorrection;
        // 中心点を反映
        transform.position = CenterPoint;
    }
}
