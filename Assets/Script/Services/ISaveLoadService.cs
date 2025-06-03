using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveLoadService
{
    void Save(SaveData data);
    SaveData Load();
}
