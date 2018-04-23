using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{

    [SerializeField]
    private float m_jumpPadPower;       //  ジャンプパワー

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //  接触判定
    void OnCollisionEnter(Collision other)
    {
        Debug.Log("ジャンプ！！！！！！！");

        //  ヒットしたオブジェクトがプレイヤーだったら
        if (other.transform.tag == "Player")
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * m_jumpPadPower);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("ジャンプ！！！！！！！");

        //  ヒットしたオブジェクトがプレイヤーだったら
        if (other.transform.tag == "Player")
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * m_jumpPadPower);
        }
    }
}