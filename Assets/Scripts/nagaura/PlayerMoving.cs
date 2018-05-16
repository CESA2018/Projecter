using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    // 移動速度
    [SerializeField]
    private float MOVING_SPEED;
    //// 初期速度
    //[SerializeField]
    //private float INITIAL_SPEED;
    //// 最高速度
    //[SerializeField]
    //private float MAXIMUM_SPEED;
    //// 加速度
    //[SerializeField]
    //private float ACCELERATION;
    // 減速度
    [SerializeField]
    private float DECELERATION;
    // ジャンプ力
    [SerializeField]
    private float JUMP_POWER;
    // 重力加速度
    [SerializeField]
    private float GRAVITY_ACCELERATION;
    // マウス感度
    [SerializeField]
    private float SENSITIVITY;
    // Rigidbody
    [SerializeField]
    private Rigidbody m_rigidbody;
    // メインカメラ
    [SerializeField]
    private GameObject m_camera;
    // プレイヤーの中心から足元までの長さ
    private float m_lengthFromCenter;
    // 移動速度
    public Vector3 m_movingVelocity;
    // 接地しているかどうか
    private bool m_isGrounded;

    // Use this for initialization
    void Start()
    {
        // 中心から足元までの長さを計算する
        m_lengthFromCenter = transform.position.y + (transform.localScale.y * gameObject.GetComponent<BoxCollider>().center.y);
        // Rigidbodyの重力を無効にする
        m_rigidbody.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        // 接地しているか判定する
        ConfirmGrounding();
        // 移動処理
        Moving();
        // 回転処理
        Rotate();
        // 接地していなかったら重力落下処理
        if (!m_isGrounded) FallingByGravity();
    }

    /// <summary>
    /// 接地しているか判定する
    /// </summary>
    public void ConfirmGrounding()
    {
        // 足元へのレイ
        Ray ray = new Ray(transform.position, -transform.up);
        // 衝突情報
        RaycastHit hit;
        // レイを飛ばして衝突判定を行う
        if (Physics.Raycast(ray, out hit))
        {
            // 接地しているか判定する
            m_isGrounded = hit.distance <= m_lengthFromCenter;
        }
        else
        {
            // レイが衝突しなかったら接地していない
            m_isGrounded = false;
        }
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    private void Moving()
    {
        m_movingVelocity = m_isGrounded ? Vector3.zero : new Vector3(0, m_movingVelocity.y, 0);
        // Wボタンを押下している
        if (Input.GetKey(KeyCode.W))
        {
            m_movingVelocity += transform.forward * MOVING_SPEED;
        }
        // Sボタンを押下している
        if (Input.GetKey(KeyCode.S))
        {
            m_movingVelocity += -transform.forward * MOVING_SPEED;
        }
        // Dボタンを押下している
        if (Input.GetKey(KeyCode.D))
        {
            m_movingVelocity += transform.right * MOVING_SPEED;
        }
        // Aボタンを押下している
        if (Input.GetKey(KeyCode.A))
        {
            m_movingVelocity += -transform.right * MOVING_SPEED;
        }
        // スペースボタンを押下している
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (m_isGrounded)
            {
                m_movingVelocity += transform.up * JUMP_POWER;
            }
        }
        // 減速する
        //Decelerate();
        // 速度を代入
        m_rigidbody.velocity = m_movingVelocity;
    }

    /// <summary>
    /// 減速処理
    /// </summary>
    private void Decelerate()
    {
        //縦方向の減速
        //横方向の減速
    }

    /// <summary>
    /// 回転処理
    /// </summary>
    public void Rotate()
    {
        // マウスの横移動量
        float mouseMoveX = Input.GetAxis("Mouse X") * SENSITIVITY;
        // マウスの縦移動量
        float mouseMoveY = Input.GetAxis("Mouse Y") * SENSITIVITY;
        // プレイヤーを横に回転する
        m_rigidbody.transform.rotation *= Quaternion.Euler(new Vector3(0.0f, mouseMoveX, 0.0f));
        // 視点を縦に回転する
        m_camera.transform.rotation *= Quaternion.Euler(new Vector3(-mouseMoveY, 0.0f, 0.0f));
    }

    /// <summary>
    /// 重力落下処理
    /// </summary>
    public void FallingByGravity()
    {
        // プレイヤーの下方向に重力加速する
        m_movingVelocity += (GRAVITY_ACCELERATION * Time.deltaTime) * transform.up;
    }
}
