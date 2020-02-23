using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCharacter : MonoBehaviour
{
    public Parameter[] parameters;

    public Image image;

    public Text name, hp, atk, speed;
    // Start is called before the first frame update
    void Start()
    {
        Select(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Select(int n)
    {
        image.sprite = parameters[n].sprite;
        name.text = parameters[n].name;
        hp.text = parameters[n].maxHp.ToString();
        atk.text = parameters[n].atk[0].ToString();
        speed.text = parameters[n].speed.ToString();

        SaveManager.save.characterNum = n;
    }
}
