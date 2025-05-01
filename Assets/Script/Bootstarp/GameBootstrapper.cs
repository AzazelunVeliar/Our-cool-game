using UnityEngine;

public class GameBootstrapper : MonoBehaviour
{
    public static ISaveLoadService SaveService;
    public static SaveLoadRepository SaveRepo;

    void Awake()
    {
        DontDestroyOnLoad(this);
        SaveService = new SaveLoadService();
        SaveRepo = new SaveLoadRepository(SaveService);
    }
}
