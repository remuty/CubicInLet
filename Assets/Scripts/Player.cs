using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField] private float speed = 5f,jumpForce = 200f;

    private Rigidbody2D rb;

    private SpriteRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        if (photonView.IsMine)
        {
            GameObject.FindWithTag("MainCamera").transform.parent = this.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 自身が生成したオブジェクトだけに移動処理を行う
        if (photonView.IsMine)
        {
            // 入力方向（ベクトル）を正規化する
            var direction = new Vector2(Input.GetAxis("Horizontal"), 0).normalized;
            if (direction.x > 0 && !renderer.flipX)
            {
                renderer.flipX = true;
            }
            else if (direction.x < 0 && renderer.flipX)
            {
                renderer.flipX = false;
            }

            // 移動速度を時間に依存させて、移動量を求める
            var dv = speed * Time.deltaTime * direction;
            transform.Translate(dv.x, dv.y, 0f);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                this.rb.AddForce(transform.up * this.jumpForce);
            }
        }
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // 自身側が生成したオブジェクトの場合はデータを送信する
            stream.SendNext(renderer.flipX);
        }
        else
        {
            // 他プレイヤー側が生成したオブジェクトの場合は受信したデータから更新する
            renderer.flipX = (bool)stream.ReceiveNext();
        }
    }
}
