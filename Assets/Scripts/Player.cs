using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField] private float speed = 2f,jumpForce = 200f;
    [SerializeField] private Effect[] effects;

    private Rigidbody2D rb;
    private SpriteRenderer renderer;
    private Animator animator;

    private bool isJump;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        if (photonView.IsMine)
        {
            GameObject.FindWithTag("MainCamera").transform.parent = this.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 自身が生成したオブジェクトだけに処理を行う
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

            //ジャンプ
            if (Input.GetKeyDown(KeyCode.Space) && !isJump)
            {
                isJump = true;
                this.rb.AddForce(transform.up * this.jumpForce);
            }

            PlayAttack();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJump = false;
        }
    }

    void PlayAttack()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Attack(0);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Attack(1);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Attack(2);
        }

        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            animator.SetInteger("Attack", -1);
        }
    }

    void Attack(int n)
    {
        animator.SetInteger("Attack", n);

        var pos = transform.position;
        if (renderer.flipX)
        {
            pos.x += 1;
        }
        else
        {
            pos.x -= 1;
        }

        //RPCで実行
        photonView.RPC(nameof(FireEffect), RpcTarget.All,
            n, pos, renderer.flipX);
    }

    [PunRPC]
    void FireEffect(int n, Vector3 pos, bool isFlip)
    {
        var effect = Instantiate(effects[n]);
        effect.Init(pos, isFlip, this.gameObject);
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
