using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var enemy = GameObject.Find("Dragon(Clone)");
        if (enemy == null)
        {
            Instantiate(prefab, transform.position, Quaternion.identity);
        }
    }
}
