using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingObject : MonoBehaviour
{
    [SerializeField] private float speed, time;
    [SerializeField] private bool canPierce;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var pos = transform.position;
        if (GetComponent<SpriteRenderer>().flipX)
        {
            pos.x += speed;
        }
        else
        {
            pos.x -= speed;
        }
        transform.position = pos;

        Destroy(gameObject, time);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (!canPierce)
            {
                Destroy(gameObject);
            }
        }
    }
}
