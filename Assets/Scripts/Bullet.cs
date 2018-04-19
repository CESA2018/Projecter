using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private Rigidbody rb;           //  リジッドボディ

    [SerializeField]
    private GameObject m_mirror;    //  生成する鏡

    public float m_speed = 1.0f;    //  弾の速度

    private GameObject m_pGun; //  打ち出す銃のオブジェクト

    // Use this for initialization
    void Start () {
        //  リジットボディの取得
        rb = gameObject.GetComponent<Rigidbody>();

        //  ゲームマネージャーの取得
        m_pGun = GameObject.Find("GameManager");

    }

// Update is called once per frame
void Update () {
        //  isKinematic
        rb.transform.Translate(Vector3.forward * m_speed);

        //rb.velocity = Vector3.forward * m_speed;
	}



    //  ヒットしたオブジェクトが壁だったら鏡を生成する
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Stage")
        {
            //  Rayで取得したオブジェクトを調べる
            RaycastHit hit =  m_pGun.GetComponent<ProjecterGun>().m_rayHitObject;

            //  既存の鏡を消す
            GameObject oldMirror = GameObject.Find("Mirror");
            if (oldMirror != null)
            {
                Destroy(oldMirror.gameObject);
            }

            ///  法線を取得して鏡を壁と垂直に置く
            GameObject newMirror = GameObject.Instantiate(m_mirror);
            newMirror.name = "Mirror";
            newMirror.transform.position = hit.point;
            newMirror.transform.rotation = Quaternion.LookRotation(-hit.normal);
        }

        //  なにかに当たったら弾が消失
        Destroy(gameObject);

    }


}
