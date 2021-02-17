using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>カメラ位置</summary>
    public GameObject m_CameraPoint;

    /// <summary>通常状態の歩く速度</summary>
    public float m_Normal_WalkSpeed = 10.0f;
    /// <summary>ダメージ状態の歩く速度</summary>
    public float m_Damage_WalkSpeed = 5.0f;
    /// <summary>通常状態の回転速度</summary>
    public float m_Normal_RotateSpeed = 20.0f;
    /// <summary>ダメージ状態の回転速度</summary>
    public float m_Damage_RotateSpeed = 10.0f;

    /// <summary>状態</summary>
    enum State
    {
        Normal,
        Damage,
    }

    /// <summary>メインカメラ</summary>
    GameObject m_MainCamera;

    /// <summary>キャラクターコントローラー</summary>
    CharacterController m_CharacterController;
    /// <summary>アニメーター</summary>
    Animator m_Animator;

    /// <summary>状態</summary>
    State m_State = State.Normal;

    /// <summary>重力加速度</summary>
    float m_Gravity = -9.81f;

    // 通常状態のアニメーション
    /// <summary>通常状態のアニメーションのX軸の移動量</summary>
    int s_Normal_VelocityX_AnimeHash = Animator.StringToHash("VelocityX");
    /// <summary>通常状態のアニメーションのZ軸の移動量</summary>
    int s_Normal_VelocityZ_AnimeHash = Animator.StringToHash("VelocityZ");

    // ダメージ状態のアニメーション
    /// <summary>ダメージ状態のアニメーションのX軸の移動量</summary>
    int s_Damage_VelocityX_AnimeHash = Animator.StringToHash("VelocityX");
    /// <summary>ダメージ状態のアニメーションのZ軸の移動量</summary>
    int s_Damage_VelocityZ_AnimeHash = Animator.StringToHash("VelocityZ");

    void Start() {
        // キャラクターコントローラーの参照
        m_CharacterController = GetComponent<CharacterController>();
        // アニメーターの参照
        m_Animator = GetComponent<Animator>();
        // メインカメラの参照
        m_MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        // メインカメラの位置を設定
        m_MainCamera.transform.position = m_CameraPoint.transform.position;
    }

    void Update() {
        // 状態ごとの更新
        switch (m_State)
        {
            case State.Normal: Normal(); break;
            case State.Damage: Damage(); break;
        }
    }

    /// <summary>通常状態</summary>
    void Normal() {
        // プレイヤーの正面向きのベクトルを取得
        Vector3 forward = m_MainCamera.transform.forward;
        // Y軸のを無視して水平にする
        forward.y = 0.0f;
        // 移動量
        Vector3 Velocity = forward * Input.GetAxis("Vertical") * m_Normal_WalkSpeed +
                           m_MainCamera.transform.right * Input.GetAxis("Horizontal") * m_Normal_WalkSpeed;

        // 通常の歩きのアニメーション
        m_Animator.SetFloat(s_Normal_VelocityX_AnimeHash, Velocity.x);
        m_Animator.SetFloat(s_Normal_VelocityZ_AnimeHash, Velocity.z);

        // 回転量
        float Rotate = Input.GetAxis("AngleHorizontal") * m_Normal_RotateSpeed * Time.deltaTime;
        // 回転を反映
        transform.Rotate(0, Rotate, 0);

        // 地面に触れている場合
        if (m_CharacterController.isGrounded) {
            Velocity.y = 0.0f;
        }

        // 重力を加える
        Velocity.y += m_Gravity * Time.deltaTime;
        // 現在のフレームの移動量
        Vector3 movement = Velocity * Time.deltaTime;
        // 移動
        // m_CharacterController.Move(movement);
    }

    /// <summary>ダメージ状態</summary>
    void Damage() {
        // プレイヤーの正面向きのベクトルを取得
        Vector3 forward = m_MainCamera.transform.forward;
        // Y軸のを無視して水平にする
        forward.y = 0.0f;
        // 移動量
        Vector3 Velocity = forward * Input.GetAxis("Vertical") * m_Damage_WalkSpeed +
                           m_MainCamera.transform.right * Input.GetAxis("Horizontal") * m_Damage_WalkSpeed;

        // ダメージ状態の歩きのアニメーション
        m_Animator.SetFloat(s_Damage_VelocityX_AnimeHash, Velocity.x);
        m_Animator.SetFloat(s_Damage_VelocityZ_AnimeHash, Velocity.z);

        // 回転量
        float Rotate = Input.GetAxis("AngleHorizontal") * m_Damage_RotateSpeed * Time.deltaTime;
        // 回転を反映
        transform.Rotate(0, Rotate, 0);

        // 地面に触れている場合
        if (m_CharacterController.isGrounded) {
            Velocity.y = 0.0f;
        }

        // 重力を加える
        Velocity.y += m_Gravity * Time.deltaTime;
        // 現在のフレームの移動量
        Vector3 movement = Velocity * Time.deltaTime;
        // 移動
        m_CharacterController.Move(movement);
    }

    /// <summary>プレイヤーとの当たり判定</summary>
    /// <param name="hit">当たったオブジェクト</param>
    void OnControllerColliderHit(ControllerColliderHit hit) {
    }
}
