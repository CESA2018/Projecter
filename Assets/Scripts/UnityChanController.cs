using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanController : MonoBehaviour {

    Rigidbody m_rigidbody;

    private bool _jumpState = false;

    [SerializeField]
    private GameObject m_camera;

    private CharacterController _Controller;

    private Vector3 _moveDirection;

    [SerializeField]
    private float _jumpPower = 1f;


    // 速度
    //[SerializeField]
    private Vector3 _speed = new Vector3(0, 0, 0);

    // 制限速度
    [SerializeField]
    private float _limit = 0.8f;

    // 加速度
    [SerializeField]
    private Vector3 _accel = new Vector3(0.1f, 0, 0.1f);

    // 回転量
    private float _rotBuf;

    // Use this for initialization
    void Start()
    {
        _Controller = GetComponent<CharacterController>();

        // 自分のRigidbodyを取ってくる
        m_rigidbody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Deceleration();
        Jump();
        AddGravity();

        float sensitivity = 2.0f; // いわゆるマウス感度
        float mouse_move_x = Input.GetAxis("Mouse X") * sensitivity;
        float mouse_move_y = Input.GetAxis("Mouse Y") * sensitivity;

        _rotBuf = mouse_move_x;

        // マウスで首を左右に回す
        m_rigidbody.transform.rotation *= Quaternion.Euler(new Vector3(0.0f, mouse_move_x, 0.0f));
        m_camera.transform.rotation *= Quaternion.Euler(new Vector3(-mouse_move_y, 0.0f, 0.0f));

    }

    
    private void Move()
    {
        // 移動処理
        // Sボタンを押下している
        if (Input.GetKey(KeyCode.S) && Mathf.Abs(_speed.z) <= Mathf.Abs(_limit))
        {
            _speed.z -= _accel.z;
        }
        // Wボタンを押下している
        if (Input.GetKey(KeyCode.W) && Mathf.Abs(_speed.z) <= Mathf.Abs(_limit))
        {
            _speed.z += _accel.z;
        }
        // Dボタンを押下している
        if (Input.GetKey(KeyCode.D) && Mathf.Abs(_speed.x) <= Mathf.Abs(_limit))
        {
            _speed.x += _accel.x;
        }
        // Aボタンを押下している
        if (Input.GetKey(KeyCode.A) && Mathf.Abs(_speed.x) <= Mathf.Abs(_limit))
        {
            _speed.x -= _accel.x;
        }

        // 速度を代入
        transform.Translate(_speed.x, 0, _speed.z);

        if (!Input.GetKey(KeyCode.A) && (!Input.GetKey(KeyCode.D)))
        {
            if (Mathf.Abs(_speed.x) <= 0.01f)
            {
                _speed.x = 0f;
            }
        }

        if (!Input.GetKey(KeyCode.W) && (!Input.GetKey(KeyCode.S)))
        {
            if (Mathf.Abs(_speed.z) <= 0.01f)
            {
                _speed.z = 0f;
            }
        }

    }

    // ジャンプ処理
    private void Jump()
    {
        if (_Controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _moveDirection.y = _jumpPower;

                _jumpState = true;
            }
            else
            {
                _jumpState = false;
            }
        }
        //Debug.Log(_jumpState);
    }

    // 重力処理
    private void AddGravity()
    {
        _moveDirection.y -= 10 * Time.deltaTime;
        _Controller.Move(_moveDirection * Time.deltaTime);

        if (_moveDirection.y < -10)
        {
            _moveDirection.y = -10;
        }


    }

    // 減速処理
    private void Deceleration()
    {
        if (!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            _speed.z *= 0.95f;
        }
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            _speed.x *= 0.95f;
        }
    }

    public void ResetVelocity()
    {
        _speed = new Vector3(0,0,0);
    }
}
