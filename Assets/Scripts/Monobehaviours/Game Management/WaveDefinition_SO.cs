using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave", menuName = "Wave", order = 57)]
public class WaveDefinition_SO : ScriptableObject
{
    public int waveSize;
    public int enemyLevel;
    public int moneyReward;
    public float waveDuration;
    public float delayBeforeWave;
}
