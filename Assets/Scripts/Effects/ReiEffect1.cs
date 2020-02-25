using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReiEffect1 : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private GameObject effect;
    // Start is called before the first frame update
    void Start()
    {
        var n = Random.Range(0, sprites.Length);
        GetComponent<SpriteRenderer>().sprite = sprites[n];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(effect, transform.position, Quaternion.identity);
    }
}
