using System;

[Serializable]
public class CharacterTidbitData
{
    public string Id;
    public bool IsUnlocked;

    public CharacterTidbitData(string id, bool isUnlocked)
    {
        Id = id;
        IsUnlocked = isUnlocked;
    }
}
