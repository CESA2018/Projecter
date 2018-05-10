using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // リジッドボディ
    Rigidbody m_rigidbody;

    [SerializeField]
    private GameObject m_camera;

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

    // 接地しているか
    private bool _isGround = false;

    // 下方へのRayの長さの許容値
    private float _JumpRayDis = 1;

    float moveX;

    float moveZ;

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(gameObject);
        // 自分のRigidbodyを取ってくる
        m_rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(m_rigidbody.velocity);

        CameraMove();
        Move();
        //CheckGround();
        Deceleration();
        //Jump();
    }

    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {

        // Wボタン
        if (Input.GetKey(KeyCode.W))
        {
            //moveZ = _speed.z;
            m_rigidbody.velocity = transform.forward * _speed.z;
            //m_rigidbody.AddForce(transform.forward * _speed.z);

            if (Mathf.Abs(_speed.z) <= Mathf.Abs(_limit))
            {
                // 制限速度まで加速
                _speed.z += _accel.z;
            }

            // 逆方向への入力がされたら
            if (_oldKey == KeyCode.S.ToString())
            {
                // 速度の初期化
                _speed = new Vector3(m_rigidbody.velocity.x, m_rigidbody.velocity.y, 0);
            }
        }

        // Sボタン
        if (Input.GetKey(KeyCode.S))
        {
            m_rigidbody.velocity = -transform.forward * _speed.z;

            if (Mathf.Abs(_speed.z) <= Mathf.Abs(_limit))
            {
                // 制限速度まで加速
                _speed.z += _accel.z;
            }

            // 逆方向への入力がされたら
            if (_oldKey == KeyCode.W.ToString())
            {
                // 速度の初期化
                _speed = new Vector3(m_rigidbody.velocity.x, m_rigidbody.velocity.y, 0);
            }
        }

        // Aボタン
        if (Input.GetKey(KeyCode.A))
        {
            m_rigidbody.velocity = -transform.right * _speed.x;

            if (Mathf.Abs(_speed.x) <= Mathf.Abs(_limit))
            {
                // 制限速度まで加速
                _speed.x += _accel.x;
            }

            // 逆方向への入力がされたら
            if (_oldKey == KeyCode.D.ToString())
            {
                // 速度の初期化
                _speed = new Vector3(0, m_rigidbody.velocity.y, m_rigidbody.velocity.z);
            }
        }

        // Dボタン
        if (Input.GetKey(KeyCode.D))
        {
            m_rigidbody.velocity = transform.right * _speed.x;

            if (Mathf.Abs(_speed.x) <= Mathf.Abs(_limit))
            {
                // 制限速度まで加速
                _speed.x += _accel.x;
            }

            // 逆方向への入力がされたら
            if (_oldKey == KeyCode.A.ToString())
            {
                // 速度の初期化
                _speed = new Vector3(0, m_rigidbody.velocity.y, m_rigidbody.velocity.z);
            }
        }

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
        //m_rigidbody.velocity = new Vector3(0,0,moveZ);
    }


    void CameraMove()
    {
        float sensitivity = 2.0f; // いわゆるマウス感度
        float mouse_move_x = Input.GetAxis("Mouse X") * sensitivity;
        float mouse_move_y = Input.GetAxis("Mouse Y") * sensitivity;

        _rotBuf = mouse_move_x;

        // マウスで首を左右に回す
        m_rigidbody.transform.rotation *= Quaternion.Euler(new Vector3(0.0f, mouse_move_x, 0.0f));
        m_camera.transform.rotation *= Quaternion.Euler(new Vector3(-mouse_move_y, 0.0f, 0.0f));
    }

    // ジャンプ処理
    private void Jump()
    {
        Debug.Log(_isGround);
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

    private void CheckGround()
    {
        float dis;
        // 真下にrayを飛ばす
        var ray = new Ray(transform.position + transform.up * 0.8f, -transform.up);
        Debug.DrawRay(ray.origin,-transform.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.name == "Stage")
            {
                dis = hit.distance;
                Debug.Log(dis);
                if (dis < _JumpRayDis)
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
