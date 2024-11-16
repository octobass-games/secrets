using System;
using System.Collections.Generic;

[Serializable]
public class CharacterData
{
    public string Name;
    public int Relationship;
    public List<string> CharacterTidbits;

    public CharacterData(string name, int relationship, List<string> characterTidbits)
    {
        Name = name;
        Relationship = relationship;
        CharacterTidbits = characterTidbits;
    }
}
