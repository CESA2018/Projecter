using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    // Rigidbody
    [SerializeField]
    private Rigidbody m_rigidbody;
    // メインカメラ
    [SerializeField]
    private GameObject m_camera;
    // 移動速度
    [SerializeField]
    private float MOVING_SPEED;
    // 最高速度
    [SerializeField]
    private float MAXIMUM_SPEED;
    // 加速度
    [SerializeField]
    private float ACCELERATION;
    // 減速度
    [SerializeField]
    private float DECELERATION;
    // マウス感度
    [SerializeField]
    private float SENSITIVITY;
    // 移動速度
    public Vector3 m_movingVelocity;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Moving();
        Rotate();
    }


    private void Moving()
    {
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
        // 加速する
        Accelerate();
        // 減速する
        Decelerate();
        // 速度を代入
        //m_rigidbody.velocity = m_movingVelocity;
    }

    private void Accelerate()
    {
        // Wキーを押下していて最高速度に達していないなら加速する
        if (Input.GetKey(KeyCode.W) && m_movingVelocity.z > 0 && m_movingVelocity.z <= MAXIMUM_SPEED)
        {
            m_movingVelocity.z *= ACCELERATION;
        }
    }
    // 減速処理
    private void Decelerate()
    {
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            m_movingVelocity.z *= DECELERATION;
            if (Mathf.Abs(m_movingVelocity.z) <= 0.01f)
            {
                m_movingVelocity.z = 0f;
            }
        }
        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.S))
        {
            m_movingVelocity.x *= DECELERATION;
            if (Mathf.Abs(m_movingVelocity.x) <= 0.01f)
            {
                m_movingVelocity.x = 0f;
            }
        }
    }

    public void Rotate()
    {
        float mouse_move_x = Input.GetAxis("Mouse X") * SENSITIVITY;
        float mouse_move_y = Input.GetAxis("Mouse Y") * SENSITIVITY;
        m_rigidbody.transform.rotation *= Quaternion.Euler(new Vector3(0.0f, mouse_move_x, 0.0f));
        m_camera.transform.rotation *= Quaternion.Euler(new Vector3(-mouse_move_y, 0.0f, 0.0f));
    }
}
