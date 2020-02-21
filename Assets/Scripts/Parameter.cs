using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/Create Parameter")]
public class Parameter : ScriptableObject
{
    public int maxHp, jumpPower;
    public float speed;
    public int[] atk;
    public float[] coolTime;
}
