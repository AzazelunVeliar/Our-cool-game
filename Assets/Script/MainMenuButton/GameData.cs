using UnityEngine;
using System;
using System.Collections.Generic;
[System.Serializable]
public class GameData
{
    public PlayerData playerData;
    public List<EnemyData> enemiesData;
}

[System.Serializable]
public class PlayerData
{
    public Vector3 position;
    public int hp;
}

[System.Serializable]
public class EnemyData
{
    public string enemyID;
    public Vector3 position;
    public float hp;
}
