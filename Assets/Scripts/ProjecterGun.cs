using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 鏡の弾を打ち出すクラス
public class ProjecterGun : MonoBehaviour
{

    [SerializeField]
    private GameObject m_player;        //  プレイヤー

    [SerializeField]
    private GameObject m_bullet;        //  弾

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
            //  弾を生成する
            GameObject bullet = GameObject.Instantiate(m_bullet);
            //  プレイヤーの位置に移動
            bullet.transform.position = m_player.transform.position;
            //  プレイヤーの向いてる角度に向ける
            bullet.transform.rotation = m_player.transform.rotation;



        }
    }
}
