using System.Collections.Generic;
using UnityEngine;

// The number of days after which the game difficulty will increase
[System.Serializable]
public class DifficultyThresholdDays
{
    [Header("Day settings")]
    [Tooltip("Day X, a boss will be spawned, and the waves settings will be modified")]
    public int StageWave = 0;
    public int TimeToNextDay = 200;


    [Header("Enemy settings")]
    public List<EnemyStats> Enemys = new List<EnemyStats>();

}

[System.Serializable]
public class EnemyStats
{
    public GameObject Pref;
    public int CountToSpawn;
    public int TimeToNextWave;
}









/*
 
 
 
 
 using System.Collections.Generic;
using UnityEngine;

// The number of days after which the game difficulty will increase
[System.Serializable]
public class DifficultyThresholdDays
{
    [Header("Day settings")]
    [Tooltip("Day X, a boss will be spawned, and the waves settings will be modified")]
    public int StageDay = 0;
    public int IntervalBetweenWaves = 200;

    [Tooltip("The count of waves to spawn will be chosen randomly within the specified minimum and maximum range")]
    public byte MinimumWaves = 1;
    public byte MaximumWaves = 3;


    [Header("Enemy settings")]
    public List<EnemyStats> EnemysStats = new List<EnemyStats>();

}

[System.Serializable]
public class EnemyStats
{
    public GameObject EnemyPrefab;
    public int MaxHP = 10;
    public int MinDamage = 10;
    public int MaxDamage = 20;

    [Tooltip("The count of enemies to be spawned in a single wave")]
    public int MinSpawnAmount = 10;
    public int MaxSpawnAmount = 20;
}
 
 
 
 
 
 
 */