using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]

public class BootsAmo : ScriptableObject
{
    public int maxBoots = 30;
    public int countBoots = 30;
    public float cooldownBoots = 0.3f;
    public int reloadBootsTime = 5;
    public bool isReadyToBootsShot = true;
}
