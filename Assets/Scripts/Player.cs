using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField] private Parameter parameter;
    [SerializeField] private Effect[] effects;
    [SerializeField] Sprite[] skillIcons;

    private Rigidbody2D rb;
    private SpriteRenderer renderer;
    private Animator animator;
    private GameObject hpGauge;
    private Text playerName;

    private int hp;
    private bool isJump;
    private bool[] isCoolTime;
    private float[] elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        hp = parameter.maxHp;

        rb = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        hpGauge = GameObject.Find("HpGauge");
        playerName = GetComponentInChildren<Text>();
        isCoolTime = new bool[skillIcons.Length];
        elapsedTime = new float[skillIcons.Length];

        if (photonView.IsMine)
        {
            //カメラを子オブジェクトにする
            var camera = GameObject.FindWithTag("MainCamera");
            camera.transform.parent = this.transform;
            camera.transform.localPosition = new Vector3(0, 0.8f, -10);

            var save = GameObject.Find("SaveManager");
            save.transform.parent = this.transform;
            save.transform.localPosition = new Vector3(0, 0);

            //スキルアイコンをセット
            for (var i = 0; i < skillIcons.Length; i++)
            {
                GameObject.Find("Skill" + i).GetComponent<Image>().sprite = skillIcons[i];
            }
        }

        //名前設定
        if (photonView.Owner.NickName == "")
        {
            playerName.text = parameter.name;
        }
        else
        {
            playerName.text = photonView.Owner.NickName;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 自身が生成したオブジェクトだけに処理を行う
        if (photonView.IsMine)
        {
            Move();
            PlayAttack();

            if (hp <= 0)
            {
                Destroy(gameObject);
                if (PhotonNetwork.InRoom)
                {
                    // 退室
                    PhotonNetwork.LeaveRoom();
                }
                SceneManager.LoadScene("Select");
            }
            else
            {
                HP(hp, parameter.maxHp);
            }

            CoolDown();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Attack"))
        {
            //攻撃に応じてダメージを受ける
            var s = other.gameObject.name.Replace("(Clone)", "");
            var n = int.Parse(s);

            var enemy = other.GetComponentInParent<Player>();
            hp -= enemy.parameter.atk[n];
        }
    }

    void Move()
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
        var dv = parameter.speed * Time.deltaTime * direction;
        transform.Translate(dv.x, dv.y, 0f);

        //ジャンプ
        if (Input.GetKeyDown(KeyCode.W) && !isJump)
        {
            isJump = true;
            this.rb.AddForce(transform.up * parameter.jumpPower);
        }
    }

    void PlayAttack()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Attack(0);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Attack(1);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Attack(2);
        }
    }

    public void Attack(int n)
    {
        if (!isCoolTime[n])
        {
            isCoolTime[n] = true;
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
    }

    [PunRPC]
    void FireEffect(int n, Vector3 pos, bool isFlip)
    {
        var effect = Instantiate(effects[n]);
        effect.Init(pos, isFlip, this.gameObject);
    }

    void HP(float current, int max)
    {
        hpGauge.GetComponentInChildren<Text>().text = current + "/" + max;
        hpGauge.GetComponent<Image>().fillAmount = current / max;
    }

    void CoolDown()
    {
        for (var i = 0; i < skillIcons.Length; i++)
        {
            if (isCoolTime[i])
            {
                elapsedTime[i] += Time.deltaTime;
                GameObject.Find("CoolDown" + i).GetComponent<Image>().fillAmount
                    = 1 - elapsedTime[i] / parameter.coolTime[i];
                if (elapsedTime[i] >= parameter.coolTime[i])
                {
                    isCoolTime[i] = false;
                    elapsedTime[i] = 0;
                }
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
