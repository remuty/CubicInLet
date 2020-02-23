using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayUserInfo : MonoBehaviour
{
    public Parameter[] parameters;
    // Start is called before the first frame update
    void Start()
    {
        var save = SaveManager.save;

        var n = save.characterNum;
        var character = GetComponent<Image>();
        character.sprite = parameters[n].sprite;

        var name = save.userName;
        var nameText = GetComponentInChildren<Text>();
        nameText.text = name;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
