using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightCharacter : MonoBehaviour
{
    public Button[] characters;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (var i = 0; i < characters.Length; i++)
        {
            var colors = characters[i].colors;
            var n = SaveManager.save.characterNum;
            if (i == n)
            {
                colors.normalColor = new Color(1,1,1,1);
            }
            else
            {
                colors.normalColor = new Color(1, 1, 1, 170f / 255f);
            }

            characters[i].colors = colors;
        }
    }
}
