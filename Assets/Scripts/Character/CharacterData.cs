using System;

[Serializable]
public class CharacterData
{
    public string Name;
    public int Relationship;

    public CharacterData(string name, int relationship)
    {
        Name = name;
        Relationship = relationship;
    }
}
