using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>カメラ位置</summary>
    public GameObject m_CameraPoint;
    /// <summary>プレイヤーカメラ</summary>
    public GameObject m_PlayerCamera;

    // 歩く速度・回転速度
    [SerializeField,Header("通常状態の歩く速度")]
    float m_NormalWalkSpeed = 0.0f;
    [SerializeField,Header("通常状態の走る速度")]
    float m_NormalRunSpeed = 0.0f;
    [SerializeField,Header("ダメージ状態の歩く速度")]
    float m_DamageWalkSpeed = 0.0f;
    [SerializeField,Header("通常状態の回転速度")]
    float m_NormalRotateSpeed = 0.0f;
    [SerializeField,Header("ダメージ状態の回転速度")]
    float m_DamageRotateSpeed = 0.0f;

    // SE
    [SerializeField,Header("歩きのSE")]
    AudioClip m_WalkSE;
    [SerializeField,Header("走りのSE")]
    AudioClip m_RunSE;
    [SerializeField,Header("ダメージのSE")]
    AudioClip m_DamageSE;

    /// <summary>状態</summary>
    enum State
    {
        Normal,
        Damage,
    }

    /// <summary>キャラクターコントローラー</summary>
    CharacterController m_CharacterController;
    /// <summary>アニメーター</summary>
    Animator m_Animator;
    /// <summary>オーディオソース</summary>
    AudioSource m_AudioSource;

    /// <summary>状態</summary>
    State m_State = State.Normal;

    /// <summary>重力加速度</summary>
    const float m_Gravity = -9.81f;
    /// <summary>通常状態の回転量</summary>
    Vector3 m_NormalRotate = new Vector3(0.0f, 0.0f, 0.0f);
    /// <summary>ダメージ状態の回転量</summary>
    Vector3 m_DamageRotate = new Vector3(0.0f, 0.0f, 0.0f);

    /// <summary>複数のコントローラーの名前</summary>
    string[] m_CacheJoysticNames;

    // 通常状態のアニメーション
    /// <summary>通常状態のアニメーションのX軸の移動量</summary>
    int s_NormalWalkVelocityXAnimeHash = Animator.StringToHash("VelocityX");
    /// <summary>通常状態のアニメーションのZ軸の移動量</summary>
    int s_NormalWalkVelocityZAnimeHash = Animator.StringToHash("VelocityZ");

    // ダッシュ状態のアニメーション
    int s_NormalRunInputValue = Animator.StringToHash("RunInputValue");

    // ダメージ状態のアニメーション
    /// <summary>ダメージ状態のアニメーションのX軸の移動量</summary>
    int s_DamageVelocityXAnimeHash = Animator.StringToHash("VelocityX");
    /// <summary>ダメージ状態のアニメーションのZ軸の移動量</summary>
    int s_DamageVelocityZAnimeHash = Animator.StringToHash("VelocityZ");

    void Start()
    {
        // キャラクターコントローラーの参照
        m_CharacterController = GetComponent<CharacterController>();
        // アニメーターの参照
        m_Animator = GetComponent<Animator>();
        // サウンドソースの参照
        m_AudioSource = GetComponent<AudioSource>();
        // コントローラーの名前を取得
        m_CacheJoysticNames = Input.GetJoystickNames();
        // カーソルを設定
        // カーソルを非表示にする
        CursorVisibe();
        // カーソルをロック
        Cursor.lockState = CursorLockMode.Locked;
        // カーソルを画面内にする
        Cursor.lockState = CursorLockMode.Confined;

    }

    void Update()
    {
        // 状態ごとの更新
        switch (m_State)
        {
            case State.Normal: Normal(); break;
            case State.Damage: Damage(); break;
        }

        // 共通する更新
        // カーソルの表示・非表示
        if (Input.GetKeyDown(KeyCode.Tab)) {
            CursorVisibe();
        }
    }

    /// <summary>通常状態の更新</summary>
    void Normal()
    {
        // 現在のコントローラーの名前を取得
        string[] TheCurrentGameController = Input.GetJoystickNames();

        // プレイヤーの正面向きのベクトルを取得
        Vector3 forward = m_PlayerCamera.transform.forward;

        // Y軸のを無視して水平にする
        forward.y = 0.0f;
        // 移動量
        Vector3 Velocity;

        // 走る時の左シフトキーの入力値
        float RunInputValue = Input.GetAxis("Run");

        // 入力値が0.9以上だった場合、走る
        if (RunInputValue >= 0.9f)
        {
            Velocity = forward * Input.GetAxis("Vertical") * m_NormalRunSpeed +
                           m_PlayerCamera.transform.right * Input.GetAxis("Horizontal") * m_NormalRunSpeed;

            // 通常状態の走りのアニメーション
            m_Animator.SetFloat(s_NormalRunInputValue, RunInputValue);
            m_Animator.SetFloat(s_NormalWalkVelocityXAnimeHash, Input.GetAxis("Horizontal"));
            m_Animator.SetFloat(s_NormalWalkVelocityZAnimeHash, Input.GetAxis("Vertical"));
        }
        else
        {
            Velocity = forward * Input.GetAxis("Vertical") * m_NormalWalkSpeed +
                           m_PlayerCamera.transform.right * Input.GetAxis("Horizontal") * m_NormalWalkSpeed;

            // 通常の歩きのアニメーション
            m_Animator.SetFloat(s_NormalWalkVelocityXAnimeHash, Input.GetAxis("Horizontal"));
            m_Animator.SetFloat(s_NormalWalkVelocityZAnimeHash, Input.GetAxis("Vertical"));
        }

        // コントローラーが接続されている場合
        if(m_CacheJoysticNames.Length < TheCurrentGameController.Length)
        {
            // コントローラーの回転量
            m_NormalRotate.y = Input.GetAxis("AngleHorizontal") * m_NormalRotateSpeed * Time.deltaTime;
        }
        // コントローラーが接続されていない場合
        else
        {
            // マウスの回転量
            m_NormalRotate.y = (Input.GetAxis("AngleMouseX") * m_NormalRotateSpeed +
                                Input.GetAxis("AngleMouseY") * m_NormalRotateSpeed)  * Time.deltaTime;
        }

        // 回転を反映
        transform.Rotate(m_NormalRotate);

        // 地面に触れている場合
        if (m_CharacterController.isGrounded)
        {
            Velocity.y = 0.0f;
        }

        // 重力を加える
        Velocity.y += m_Gravity * Time.deltaTime;
        // 現在のフレームの移動量
        Vector3 movement = Velocity * Time.deltaTime;
        // 移動
        m_CharacterController.Move(movement);
    }

    /// <summary>ダメージ状態の更新</summary>
    void Damage()
    {
        // 現在のコントローラーの名前を取得
        string[] TheCurrentGameController = Input.GetJoystickNames();

        // プレイヤーの正面向きのベクトルを取得
        Vector3 forward = m_PlayerCamera.transform.forward;
        // Y軸のを無視して水平にする
        forward.y = 0.0f;
        // 移動量
        Vector3 Velocity = forward * Input.GetAxis("Vertical") * m_DamageWalkSpeed +
                           m_PlayerCamera.transform.right * Input.GetAxis("Horizontal") * m_DamageWalkSpeed;

        // ダメージ状態の歩きのアニメーション
        m_Animator.SetFloat(s_DamageVelocityXAnimeHash, Input.GetAxis("Horizontal"));
        m_Animator.SetFloat(s_DamageVelocityZAnimeHash, Input.GetAxis("Vertical"));

        // コントローラーが接続されている場合
        if (m_CacheJoysticNames.Length < TheCurrentGameController.Length)
        {
            // コントローラーの回転量
            m_DamageRotate.y = Input.GetAxis("AngleHorizontal") * m_NormalRotateSpeed * Time.deltaTime;
        }
        // コントローラーが接続されていない場合
        else
        {
            // マウスの回転量
            m_DamageRotate.y = Input.GetAxis("AngleMouseX") * m_NormalRotateSpeed * Time.deltaTime;
        }
        // 回転を反映
        transform.Rotate(m_DamageRotate);

        // 地面に触れている場合
        if (m_CharacterController.isGrounded)
        {
            Velocity.y = 0.0f;
        }

        // 重力を加える
        Velocity.y += m_Gravity * Time.deltaTime;
        // 現在のフレームの移動量
        Vector3 movement = Velocity * Time.deltaTime;
        // 移動
        m_CharacterController.Move(movement);
    }

    /// <summary>カーソルの表示・非表示</summary>
    void CursorVisibe()
    {
        // カーソルが表示されている場合
        if (Cursor.visible)
        {
            // カーソルを非表示
            Cursor.visible = false;
        }
        // カーソルが非表示されている場合
        else
        {
            // カーソルを表示
            Cursor.visible = true;
        }

        // カーソルがロックされている場合
        if(Cursor.lockState == CursorLockMode.Locked)
        {
            // カーソルのロックを解除
            Cursor.lockState = CursorLockMode.None;
        }
    }

    /// <summary>SEを流す</summary>
    /// <param name="PlaySE">流すSE</param>
    void PlaySE(AudioClip PlaySE)
    {
        m_AudioSource.PlayOneShot(PlaySE);
    }

    /// <summary>プレイヤーとの当たり判定</summary>
    /// <param name="hit">当たったオブジェクト</param>
    void OnControllerColliderHit(ControllerColliderHit hit) {
    }

    /// <summary>歩くSE</summary>
    public void WalkSE()
    {
        PlaySE(m_WalkSE);
    }
}
