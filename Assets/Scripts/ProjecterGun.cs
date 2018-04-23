using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 鏡の弾を打ち出すクラス
public class ProjecterGun : MonoBehaviour
{

    [SerializeField]
    private GameObject m_shotPosition;        //  プレイヤー

    [SerializeField]
    private GameObject m_bullet;        //  弾

    public RaycastHit m_rayHitObject;        //  弾が当たる直前に保存するオブジェクト

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //  右クリックで弾を発射する
        if (Input.GetMouseButtonDown(1))
        {
            //  弾が存在していたら撃てない
            if (GameObject.Find("Bullet(Clone)") == null)
            {
                //  Rayを飛ばす
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    //  タグがステージ
                    if (hit.collider.tag == "Stage")
                    {
                        m_rayHitObject = hit;
                    }
                }

                //  弾を生成する
                GameObject bullet = GameObject.Instantiate(m_bullet);
                //  プレイヤーの位置に移動
                bullet.transform.position = m_shotPosition.transform.position;
                //  プレイヤーの向いてる角度に向ける
                bullet.transform.rotation = m_shotPosition.transform.rotation;

            }
        }
    }

    public RaycastHit GetRayCastHitObject()
    {
        return m_rayHitObject;
    }
}
