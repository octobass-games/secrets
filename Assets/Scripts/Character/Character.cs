using UnityEngine;

public class Character : MonoBehaviour, Savable
{
    public CharacterDefinition CharacterDefinition;

    public void Load(SaveData saveData)
    {
        CharacterData characterData = saveData.Characters.Find(c => c.Name == CharacterDefinition.Name);

        CharacterDefinition.Relationship = characterData.Relationship;
    }

    public void Save(SaveData saveData)
    {
        CharacterData characterData = new(CharacterDefinition.Name, CharacterDefinition.Relationship);

        saveData.Characters.Add(characterData);
    }
}
