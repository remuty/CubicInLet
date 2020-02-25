using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform attackPos;
    [SerializeField] private Parameter parameter;

    public Parameter Parameter
    {
        get { return parameter; }
    }

    private GameObject target;
    private Animator animator;
    private int hp;
    private float elapsedTime;
    private bool isAttack;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        hp = parameter.maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Attack"))
        {
            if (!isAttack)
            {
                isAttack = true;
                elapsedTime = 0;
                animator.SetInteger("Act", 1);
                target = other.gameObject.transform.root.gameObject;
            }
            //攻撃に応じてダメージを受ける
            var s = other.gameObject.name.Replace("(Clone)", "");
            var n = int.Parse(s);

            var player = other.GetComponentInParent<Player>();
            hp -= player.Parameter.atk[n];
            Debug.Log(hp);
        }
    }

    void Attack()
    {
        if (isAttack)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= 15)
            {
                isAttack = false;
                animator.SetInteger("Act", 0);
            }

            var x = parameter.speed * Time.deltaTime;
            if (target.transform.position.x > attackPos.position.x)
            {
                transform.Translate(x, 0, 0f);
            }
            else
            {
                transform.Translate(-x, 0, 0f);
            }
        }
    }
}
