using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField]
    private GameObject m_player;

    [SerializeField]
    private GameObject m_mirror;

    // Use this for initialization
    void Start ()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // プレイヤーから飛ばしたレイが衝突したオブジェクトの情報
            RaycastHit phit;
            // プレイヤーから向いている方向にレイを飛ばす
            if (Physics.Raycast(ray, out phit))
            {
                if (phit.transform.tag == ("Mirror"))
                {
                    // プレイヤーから衝突点へのベクトル
                    Vector3 f = phit.point - (m_player.transform.position + new Vector3(0, 1.34f, 0));
                    //Debug.DrawRay(m_player.transform.position + new Vector3(0, 1.34f, 0), f.normalized * 100, Color.red, 60);
                    // 衝突面の法線ベクトル
                    Vector3 n = phit.normal;
                    // 這いずりベクトル
                    //Vector3 w = f - (Vector3.Dot(f, n) * n);
                    // 反射ベクトル
                    Vector3 r = f - ((2.0f * Vector3.Dot(f, n)) * n);
                    // 鏡から飛ばしたレイが衝突したオブジェクトの情報
                    RaycastHit mhit;
                    // 衝突点から反射方向にレイを飛ばす
                    if (Physics.Raycast(phit.point, r.normalized, out mhit))
                    {
                        //Debug.DrawRay(phit.point, r.normalized * 100, Color.red, 60);
                        m_player.transform.position = mhit.point;
                        if (mhit.collider.tag == "Wall")
                        {
                            Vector3 v = -r.normalized * 0.5f;
                            m_player.transform.position += v;
                        }
                        else
                        {
                            Vector3 v = -r.normalized * 1.0f;
                            m_player.transform.position += v;
                        }

                    }
                    //Debug.Log(f);
                    //Debug.Log(phit.point);
                    //Debug.Log(r);
                    //Debug.Log(mhit.point);
                }
            }
        }

        //  右クリックしたら鏡を置く
        if (Input.GetMouseButtonDown(1))
        {
            //  既存の鏡を消す
            GameObject oldMirror = GameObject.Find("Mirror");
            if (oldMirror != null)
            {
                Destroy(oldMirror.gameObject);
            }


            //  Rayを飛ばす
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //  タグがステージ
                if (hit.collider.tag == "Stage")
                {
                    //  法線を取得して鏡を壁と垂直に置く
                    GameObject newMirror = GameObject.Instantiate(m_mirror);
                    newMirror.name = "Mirror";
                    newMirror.transform.position = hit.point;
                    newMirror.transform.rotation = Quaternion.LookRotation(-hit.normal);

                }
            }
        }
    }
}