using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    private float moveSpeed;
    private float jumpPower = 400;
    private float enCountTime;
    private float enCountTimeLimit;
    private bool jumpflag = true;
    private Rigidbody2D rb2D;

    private Vector3 pos;
    private Vector3 initVector3 = new Vector3(-90f, -2.3f);
    void Start()
    {
        enCountTimeLimit = 40;
        rb2D = GetComponent<Rigidbody2D>();

        SceneSelect();
        Debug.Log("Zでジャンプ、Spaceでメニュー、左SHIFTでDebug");
    }

    void FixedUpdate()
    {
        Move();

    }
    private void Move()
    {
        pos.x = Input.GetAxis("Horizontal");
        pos.y = Input.GetAxis("Vertical");
        moveSpeed = Mathf.Clamp(moveSpeed, 5, 10);
        rb2D.AddForce(pos * moveSpeed);

        Jump();

        EnConut(pos.x, pos.y);
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Z) && jumpflag == true)
        {
            Vector2 gravityPos2 = Vector2.up;
            rb2D.AddForce(Vector2.up * jumpPower);

                jumpflag = false;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Field"))
        {
            jumpflag = true;
        }
    }
    //戦闘後の座標
    public Vector3 GetStageMapPos()
    {//エンカウントが入る　エンカウント更新　問題ない　タウンでリセット
        return transform.position = Database.instance.EnCountPosition;
    }

    public Vector3 GetRoadStageMapPos()
    {
        return transform.position = Database.instance.SavePointPosition;
    }

    //シーン別に処理分け
    void SceneSelect()
    {
        if (SceneManager.GetActiveScene().name == "ActionStage" && Database.instance.IsSavePoint == false)
        {
            transform.position = initVector3;
            GetStageMapPos();
        }
        else
        {
            GetRoadStageMapPos();//タウン内でのSaveRoadは問題ない
            Database.instance.IsSavePoint = false;
        }
        //エンカウント座標持った状態でタウンに入りエンカウント座標がリセット
        if (SceneManager.GetActiveScene().name == "Town")
        {
            transform.position = new Vector2(-6f, -0.9f);//タウンの初期座標
            Database.instance.EnCountPosition = initVector3;//アクションの初期値設定
        }
    }

    void EnConut(float x, float y)
    {
        Vector2 pos = new Vector2(x, y);

        if (pos == Vector2.zero || SceneManager.GetActiveScene().name == "Town")
        {
            //  Debug.Log(enCountTime + "STOPenCountTime");
        }
        else
        {
            enCountTime += 0.1f;
            //  Debug.Log(enCountTime + "enCountTime");
        }

        if (enCountTime >= enCountTimeLimit)
        {
            enCountTime = 0;
            Database.instance.EnCountPosition = transform.position;
            SceneChange.instance.SceneChangeType(SCENE_TYPE.Battle);
        }
    }
}