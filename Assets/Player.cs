using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player : MonoBehaviourPunCallbacks
{
    [SerializeField] private float speed = 5f,jumpForce = 200f;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // 自身が生成したオブジェクトだけに移動処理を行う
        if (photonView.IsMine)
        {
            // 入力方向（ベクトル）を正規化する
            var direction = new Vector2(Input.GetAxis("Horizontal"), 0).normalized;
            // 移動速度を時間に依存させて、移動量を求める
            var dv = speed * Time.deltaTime * direction;
            transform.Translate(dv.x, dv.y, 0f);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                this.rb.AddForce(transform.up * this.jumpForce);
            }
        }
    }
}
