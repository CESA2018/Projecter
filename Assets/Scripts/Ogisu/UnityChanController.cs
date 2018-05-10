﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnityChanController : MonoBehaviour
{

    // リジッドボディ
    Rigidbody m_rigidbody;

    [SerializeField]
    private GameObject m_camera;

    // 方向
    private Vector3 _moveDirection;

    // ジャンプ力
    [SerializeField]
    private float _jumpPower = 250f;


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

    private bool _isGround = false;

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        // 自分のRigidbodyを取ってくる
        m_rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(m_rigidbody.velocity);
        CheckGround();
        Move();
        Deceleration();
        Jump();

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

    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {
        // 移動処理
        // Sボタンを押下している
        if (Input.GetKey(KeyCode.S))
        {
            // 加速を適用
            _speed = transform.rotation * Vector3.back * -_speed.z;

            if (Mathf.Abs(_speed.z) <= Mathf.Abs(_limit))
            {
                // 制限速度まで加速
                _speed.z -= _accel.z;
            }

            if (_oldKey == KeyCode.W.ToString())
            {
                // 速度の初期化
                _speed = new Vector3(_speed.x, m_rigidbody.velocity.y, 0);
            }
        }


        // Wボタンを押下している
        if (Input.GetKey(KeyCode.W))
        {
            // 加速を適用
            _speed = transform.rotation * Vector3.forward * _speed.z;

            if (Mathf.Abs(_speed.z) <= Mathf.Abs(_limit))
            {
                // 制限速度まで加速
                _speed.z += _accel.z;
                
            }
            if (_oldKey == KeyCode.S.ToString())
            {
                // 速度の初期化
                _speed = new Vector3(_speed.x, m_rigidbody.velocity.y, 0);
            }
        }


        // Dボタンを押下している
        if (Input.GetKey(KeyCode.D))
        {
            // 加速を適用
            _speed = transform.rotation * Vector3.right * _speed.x;

            if (Mathf.Abs(_speed.x) <= Mathf.Abs(_limit))
            {
                // 制限速度まで加速
                _speed.x += _accel.x;

            }
            if (_oldKey == KeyCode.A.ToString())
            {
                // 速度の初期化
                _speed = new Vector3(0, m_rigidbody.velocity.y, _speed.z);
            }
        }


        // Aボタンを押下している
        if (Input.GetKey(KeyCode.A))
        {
            // 加速を適用
            _speed = transform.rotation * Vector3.left * -_speed.x;
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
                _speed = new Vector3(0, m_rigidbody.velocity.y, _speed.z);
            }
        }

        // 速度を代入
        //m_rigidbody.AddForce(_speed);


        // 一定速度以下ならストップ
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

        // 加速処理
        m_rigidbody.velocity = _speed;
    }

    // ジャンプ処理
    private void Jump()
    {
        if (_isGround == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_rigidbody.AddForce(Vector3.up * _jumpPower);
            }
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

    // リセット処理
    public void ResetVelocity()
    {
        _speed = new Vector3(0, 0, 0);
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
                    //Debug.Log(code);
                    _oldKey = code.ToString();
                    break;
                }
            }
        }
    }

    private void CheckGround()
    {
        float dis;
        // 真下にrayを飛ばす
        var ray = new Ray(transform.position + Vector3.up * 0.8f, Vector3.down);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.name == "Stage")
            {
                dis = hit.distance;

                if (dis < 0.9f)
                {
                    _isGround = true;
                }
                else
                {
                    _isGround = false;
                }
            }
        }
    }
}
