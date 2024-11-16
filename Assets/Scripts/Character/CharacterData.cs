using System;
using System.Collections.Generic;

[Serializable]
public class CharacterData
{
    public string Name;
    public int Relationship;
    public List<CharacterTidbitData> CharacterTidbits;

    public CharacterData(string name, int relationship, List<CharacterTidbitData> characterTidbits)
    {
        Name = name;
        Relationship = relationship;
        CharacterTidbits = characterTidbits;
    }
}
