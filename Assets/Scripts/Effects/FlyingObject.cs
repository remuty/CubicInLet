using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingObject : MonoBehaviour
{
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
            pos.x += 0.1f;
        }
        else
        {
            pos.x -= 0.1f;
        }
        transform.position = pos;

        Destroy(gameObject, 0.5f);
    }
}
