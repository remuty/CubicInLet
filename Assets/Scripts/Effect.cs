﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    [SerializeField] private float x;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator != null)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Init(Vector3 origin, bool isFlip, GameObject parent)
    {
        transform.position = origin;
        GetComponentInChildren<SpriteRenderer>().flipX = isFlip;
        this.transform.parent = parent.transform;

        //エフェクト位置調整
        if (x != 0)
        {
            if (transform.localPosition.x > 0)
            {
                x *= -1;
            }
            transform.localPosition = new Vector3(x, 0);
        }
    }
}
