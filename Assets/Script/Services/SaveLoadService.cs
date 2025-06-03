using UnityEngine;

public class SaveLoadService : ISaveLoadService
{
    private const string SaveKey = "GameSave";

    public void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(SaveKey, json);
        PlayerPrefs.Save();
    }

    public SaveData Load()
    {
        string json = PlayerPrefs.GetString(SaveKey, "{}");
        return JsonUtility.FromJson<SaveData>(json);
    }
}
