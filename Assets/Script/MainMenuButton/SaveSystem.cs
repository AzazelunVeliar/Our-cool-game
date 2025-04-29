using System.Collections.Generic;
using UnityEngine;

public class SaveSystem
{
    private SaveService saveService;
    private Player player;
    private List<Enemy> enemies;

    public SaveSystem(Player player)
    {
        this.player = player;
        saveService = new SaveService();
        enemies = new List<Enemy>(Object.FindObjectsOfType<Enemy>());
    }

    public void SaveGame()
    {
        var data = new GameData
        {
            playerData = player.GetSaveData(),
            enemiesData = new List<EnemyData>()
        };

        foreach (var enemy in enemies)
        {
            data.enemiesData.Add(enemy.GetSaveData());
        }

        saveService.Save(data); // Используем переименованный метод
    }

    public void LoadGame()
    {
        GameData data = saveService.Load(); // Используем переименованный метод
        if (data != null)
        {
            player.LoadData(data.playerData);
            
            foreach (var enemyData in data.enemiesData)
            {
                Enemy enemy = enemies.Find(e => e.enemyID == enemyData.enemyID);
                if (enemy != null)
                {
                    enemy.LoadData(enemyData);
                }
            }
        }
    }
}