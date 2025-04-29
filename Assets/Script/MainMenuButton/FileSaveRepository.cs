using System;
using System.IO;
using UnityEngine;

public class FileSaveRepository : ISaveRepository
{
    private string savePath;

    public FileSaveRepository()
    {
        savePath = Path.Combine(Application.persistentDataPath, "save.json");
    }

    public void Save(GameData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(savePath, json);
    }

    public GameData Load()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            return JsonUtility.FromJson<GameData>(json);
        }
        return null;
    }
}