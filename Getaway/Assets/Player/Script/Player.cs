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

    // SE
    /// <summary>歩きのSE</summary>
    public AudioClip m_WalkSE;
    /// <summary>走りのSE</summary>
    public AudioClip m_RunSE;
    /// <summary>ダメージのSE</summary>
    public AudioClip m_DamageSE;

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
    /// <summary>オーディオソース</summary>
    AudioSource m_AudioSource;

    /// <summary>状態</summary>
    State m_State = State.Normal;

    /// <summary>重力加速度</summary>
    float m_Gravity = -9.81f;
    /// <summary>通常状態の回転量</summary>
    Vector3 m_Normal_Rotate = new Vector3(0.0f, 0.0f, 0.0f);
    /// <summary>ダメージ状態の回転量</summary>
    Vector3 m_Damage_Rotate = new Vector3(0.0f, 0.0f, 0.0f);

    /// <summary>複数のコントローラーの名前</summary>
    string[] m_CacheJoysticNames;

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

    void Start()
    {
        // キャラクターコントローラーの参照
        m_CharacterController = GetComponent<CharacterController>();
        // アニメーターの参照
        m_Animator = GetComponent<Animator>();
        // サウンドソースの参照
        m_AudioSource = GetComponent<AudioSource>();
        // メインカメラの参照
        m_MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        // メインカメラの位置を設定
        m_MainCamera.transform.position = m_CameraPoint.transform.position;
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

    /// <summary>通常状態</summary>
    void Normal()
    {
        // 現在のコントローラーの名前を取得
        string[] TheCurrentGameController = Input.GetJoystickNames();

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

        // コントローラーが接続されている場合
        if(m_CacheJoysticNames.Length < TheCurrentGameController.Length)
        {
            // コントローラーの回転量
            m_Normal_Rotate.y = Input.GetAxis("AngleHorizontal") * m_Normal_RotateSpeed * Time.deltaTime;
        }
        // コントローラーが接続されていない場合
        else
        {
            // マウスの回転量
            m_Normal_Rotate.y = (Input.GetAxis("AngleMouseX") * m_Normal_RotateSpeed +
                                Input.GetAxis("AngleMouseY") * m_Normal_RotateSpeed)  * Time.deltaTime;
        }

        // 回転を反映
        transform.Rotate(m_Normal_Rotate);

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

    /// <summary>ダメージ状態</summary>
    void Damage()
    {
        // 現在のコントローラーの名前を取得
        string[] TheCurrentGameController = Input.GetJoystickNames();

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

        // コントローラーが接続されている場合
        if (m_CacheJoysticNames.Length < TheCurrentGameController.Length)
        {
            // コントローラーの回転量
            m_Damage_Rotate.y = Input.GetAxis("AngleHorizontal") * m_Normal_RotateSpeed * Time.deltaTime;
        }
        // コントローラーが接続されていない場合
        else
        {
            // マウスの回転量
            m_Damage_Rotate.y = Input.GetAxis("AngleMouseX") * m_Normal_RotateSpeed * Time.deltaTime;
        }
        // 回転を反映
        transform.Rotate(m_Damage_Rotate);

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
