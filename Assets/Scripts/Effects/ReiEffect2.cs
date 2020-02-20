using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReiEffect2 : MonoBehaviour
{
    [SerializeField] private GameObject childObj;
    private Vector2 pos;
    private Vector2 childPos;
    // Start is called before the first frame update
    void Start()
    {
        var isFlipX = GetComponent<SpriteRenderer>().flipX;
        childObj.GetComponent<SpriteRenderer>().flipX = isFlipX;
        pos.y = transform.position.y;
        childPos = childObj.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, pos.y);
        childObj.transform.position = childPos;
    }
}
