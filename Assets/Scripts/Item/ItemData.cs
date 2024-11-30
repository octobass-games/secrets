using System;
using UnityEngine;

[Serializable]
public class ItemData
{
    public string Name;
    public bool IsUnlocked;

    public ItemData(string name, bool isUnlocked)
    {
        Name = name;
        IsUnlocked = isUnlocked;
    }
}
