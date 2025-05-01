using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadRepository
{
    private readonly ISaveLoadService _service;

    public SaveLoadRepository(ISaveLoadService service)
    {
        _service = service;
    }

    public void SavePlayer(Player player)
    {
        var data = new SaveData
        {
            Position = player.transform.position,
            HP = player.HP,
            MP = player.MP
        };

        _service.Save(data);
    }

    public void LoadPlayer(Player player)
    {
        var data = _service.Load();
        player.transform.position = data.Position;
        player.HP = data.HP;
        player.MP = data.MP;
    }
}
