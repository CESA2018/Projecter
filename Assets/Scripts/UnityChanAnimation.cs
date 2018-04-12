using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanAnimation : MonoBehaviour {

    private Animator animator;

    // フラグ
    private const string key_isRun = "isRun";
    private const string key_isJump = "isJump";

    // Use this for initialization
    void Start () {
        // 自分のアニメーションコンポーネントの取得
        this.animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        // 矢印下ボタンを押下している
        if (Input.GetKey(KeyCode.S))
        {
            // WaitからRunに遷移する
            this.animator.SetBool(key_isRun, true);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            // WaitからRunに遷移する
            this.animator.SetBool(key_isRun, true);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            // WaitからRunに遷移する
            this.animator.SetBool(key_isRun, true);

        }
        else if (Input.GetKey(KeyCode.A))
        {
            // WaitからRunに遷移する
            this.animator.SetBool(key_isRun, true);

        }
        else
        {
            // RunからWaitに遷移する
            this.animator.SetBool(key_isRun, false);
        }

        //// Wait or Run からJumpに切り替える処理
        //// スペースキーを押下している
        //if (Input.GetKey(KeyCode.Space))
        //{
        //    // Wait or RunからJumpに遷移する
        //    this.animator.SetBool(key_isJump, true);
        //}
        //else
        //{
        //    // JumpからWait or Runに遷移する
        //    this.animator.SetBool(key_isJump, false);
        //}
    }
}
