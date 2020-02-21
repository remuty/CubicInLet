using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SkillButtonClick(int n)
    {
        var player = GameObject.FindWithTag("Player");
        var playerCs = player.GetComponent<Player>();
        playerCs.Attack(n);
    }
}
