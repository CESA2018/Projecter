using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnityChanController : MonoBehaviour {

    // リジッドボディ
    Rigidbody m_rigidbody;

    // ジャンプフラグ
    private bool _jumpState = false;

    [SerializeField]
    private GameObject m_camera;

    private CharacterController _Controller;

    // 方向
    private Vector3 _moveDirection;

    // ジャンプ力
    [SerializeField]
    private float _jumpPower = 1f;


    // 現在の速度
    private Vector3 _speed = new Vector3(0, 0, 0);

    // 制限速度
    [SerializeField]
    private float _limit = 0.8f;

    // 加速度
    [SerializeField]
    private Vector3 _accel = new Vector3(0.1f, 0, 0.1f);

    // 回転量
    private float _rotBuf;

    // 前のフレームのキー情報
    private string _oldKey; 

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

        // キー情報の更新
        DownKeyCheck();
    }


    private void Move()
    {
        // 移動処理
        //// Sボタンを押下している
        //if (Input.GetKey(KeyCode.S))
        //{
        //    // 前フレームと同じキーを押しているなら後方に加速
        //    if (_oldKey == KeyCode.S.ToString())
        //    {
        //        if (Mathf.Abs(_speed.z) <= Mathf.Abs(_limit))
        //        {
        //            // 制限速度まで加速
        //            _speed.z -= _accel.z;
        //        }
        //    }
        //    else
        //    {
        //        // 速度の初期化
        //        _speed = new Vector3(_speed.x, _speed.y, 0);
        //    }
        //}

        // Sボタンを押下している
        if (Input.GetKey(KeyCode.S))
        {
            if (Mathf.Abs(_speed.z) <= Mathf.Abs(_limit))
            {
                // 制限速度まで加速
                _speed.z -= _accel.z;
            }

            if (_oldKey == KeyCode.W.ToString())
            {
                // 速度の初期化
                _speed = new Vector3(_speed.x, _speed.y, 0);
            }
        }


        // Wボタンを押下している
        //if (Input.GetKey(KeyCode.W))
        //{
        //    // 前フレームと同じキーを押しているなら前方に加速
        //    if (_oldKey == KeyCode.W.ToString())
        //    {
        //        if (Mathf.Abs(_speed.z) <= Mathf.Abs(_limit))
        //        {
        //            // 制限速度まで加速
        //            _speed.z += _accel.z;
        //        }
        //    }
        //    else
        //    {
        //        // 速度の初期化
        //        _speed = new Vector3(_speed.x, _speed.y, 0);
        //    }
        //}

        if (Input.GetKey(KeyCode.W))
        {
            if (Mathf.Abs(_speed.z) <= Mathf.Abs(_limit))
            {
                // 制限速度まで加速
                _speed.z += _accel.z;
            }
            if (_oldKey == KeyCode.S.ToString())
            {
                // 速度の初期化
                _speed = new Vector3(_speed.x, _speed.y, 0);
            }
        }


        // Dボタンを押下している
        //if (Input.GetKey(KeyCode.D))
        //{
        //    // 前フレームと同じキーを押しているなら右方に加速
        //    if (_oldKey == KeyCode.D.ToString())
        //    {
        //        if (Mathf.Abs(_speed.x) <= Mathf.Abs(_limit))
        //        {
        //            // 制限速度まで加速
        //            _speed.x += _accel.x;
        //        }
        //    }
        //    else
        //    {
        //        // 速度の初期化
        //        _speed = new Vector3(0, _speed.y, _speed.z);
        //    }
        //}

        if (Input.GetKey(KeyCode.D))
        {
            if (Mathf.Abs(_speed.x) <= Mathf.Abs(_limit))
            {
                // 制限速度まで加速
                _speed.x += _accel.x;
            }
            if (_oldKey == KeyCode.A.ToString())
            {
                // 速度の初期化
                _speed = new Vector3(0, _speed.y, _speed.z);
            }
        }


        // Aボタンを押下している
        //if (Input.GetKey(KeyCode.A))
        //{
        //    // 前フレームと同じキーを押しているなら左方に加速
        //    if (_oldKey == KeyCode.A.ToString())
        //    {
        //        if (Mathf.Abs(_speed.x) <= Mathf.Abs(_limit))
        //        {
        //            // 制限速度まで加速
        //            _speed.x -= _accel.x;
        //        }
        //    }
        //    else
        //    {
        //        // 速度の初期化
        //        _speed = new Vector3(0, _speed.y, _speed.z);
        //    }
        //}

        if (Input.GetKey(KeyCode.A))
        {
            // 前フレームと同じキーを押しているなら左方に加速
            {
            if (Mathf.Abs(_speed.x) <= Mathf.Abs(_limit))
            {
                // 制限速度まで加速
                _speed.x -= _accel.x;
            }
            }
            if (_oldKey == KeyCode.D.ToString())
            {
                // 速度の初期化
                _speed = new Vector3(0, _speed.y, _speed.z);
            }
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

        //Debug.Log(_moveDirection);
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

    // リセット処理
    public void ResetVelocity()
    {
        _speed = new Vector3(0,0,0);
    }

    // キー入力チェック関数
    void DownKeyCheck()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode code in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(code))
                {
                    if (code == KeyCode.Space)
                    {
                        break;
                    }
                    //処理を書く
                    Debug.Log(code);
                    _oldKey = code.ToString();
                    break;
                }
            }
        }
    }
}
