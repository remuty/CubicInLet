using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    [SerializeField] private float x,y;
    [SerializeField] private GameObject childObj;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            Destroy(gameObject);
        }
    }

    public void Init(Vector3 origin, bool isFlip, GameObject parent)
    {
        transform.position = origin;
        GetComponentInChildren<SpriteRenderer>().flipX = isFlip;
        this.transform.parent = parent.transform;

        if (transform.localPosition.x > 0)
        {
            childObj.transform.localPosition = new Vector3(-x, y);
        }
        else
        {
            childObj.transform.localPosition = new Vector3(x, y);
        }
    }
}
