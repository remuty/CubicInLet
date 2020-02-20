using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/Create Parameter")]
public class Parameter : ScriptableObject
{
    public int maxHp, speed, jumpPower;
    public int[] atk;
}
