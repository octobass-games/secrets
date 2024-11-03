using UnityEngine;

public interface Savable
{
    void Save(SaveData saveData);

    void Load(SaveData saveData);
}
