using System;

[Serializable]
public class StampData
{
    public string Id;
    public bool IsUnlocked;

    public StampData(string id, bool isUnlocked)
    {
        Id = id;
        IsUnlocked = isUnlocked;
    }
}
