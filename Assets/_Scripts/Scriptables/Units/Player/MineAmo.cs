using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class MineAmo : ScriptableObject
{
    public int countMine = 5;
    public int maxMine = 5;
    public int reloadMineTime = 15;
    public float cooldownMine = 1f;
    public bool isReadyToMineShot = true;
}
