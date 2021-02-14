using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>カメラの位置</summary>
    public GameObject m_CameraPoint;

    /// <summary>歩く速度</summary>
    public float m_WalkSpeed = 10.0f;
    /// <summary>回転速度</summary>
    public float m_RotateSpeed = 20.0f;

    /// <summary>メインカメラ</summary>
    GameObject m_MainCamera;

    /// <summary>キャラクターコントローラー</summary>
    CharacterController m_CharacterController;

    /// <summary>重力加速度</summary>
    float m_Gravity = -9.81f;
    /// <summary>移動量のY軸</summary>
    float m_VelocityY = 0.0f;

    void Start() {
        // キャラクターコントローラーの参照
        m_CharacterController = GetComponent<CharacterController>();
        // カメラの参照
        m_MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update() {

        // カメラの位置をプレイヤーの眼の位置に変更
        m_MainCamera.transform.position = m_CameraPoint.transform.position;

        // プレイヤーの正面向きのベクトルを取得
        Vector3 forward = transform.forward;
        // Y軸のを無視して水平にする
        forward.y = 0.0f;
        // 移動量
        Vector3 Velocity = forward * Input.GetAxis("Vertical") * m_WalkSpeed + 
                           transform.right * Input.GetAxis("Horizontal") * m_WalkSpeed;

        // 回転量
        float Rotate = Input.GetAxis("AngleHorizontal") * m_RotateSpeed * Time.deltaTime;
        float CameraRotate = Input.GetAxis("AngleHorizontal") * m_RotateSpeed * Time.deltaTime;
        // 回転を反映
        transform.Rotate(0, Rotate, 0);
        m_MainCamera.transform.Rotate(0, CameraRotate, 0);

        // 地面に触れている場合
        if (m_CharacterController.isGrounded) {
            m_VelocityY = 0.0f;
        }

        // 重力を加える
        m_VelocityY += m_Gravity * Time.deltaTime;
        Velocity.y = m_VelocityY;
        

        // 現在のフレームの移動量
        Vector3 movement = Velocity * Time.deltaTime;
        // 移動
        m_CharacterController.Move(movement);
    }
}
