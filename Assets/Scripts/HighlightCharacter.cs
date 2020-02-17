using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightCharacter : MonoBehaviour
{
    [SerializeField] private GameObject[] character;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (var i = 0; i < character.Length; i++)
        {
            var btn = character[i].GetComponent<Button>();
            var colors = btn.colors;
            var n = SelectCharacter.characterNum;
            if (i == n)
            {
                colors.normalColor = new Color(1,1,1,1);
            }
            else
            {
                colors.normalColor = new Color(1, 1, 1, 170f / 255f);
            }

            btn.colors = colors;
        }
    }
}
