using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Character : MonoBehaviour, Savable
{
    public CharacterDefinition CharacterDefinition;

    private List<Dialogue> Dialogues;

    void Awake()
    {
        Dialogues = CharacterDefinition.Dialogues.Select(d => JsonUtility.FromJson<Dialogue>(d.text)).ToList();
    }

    public void Converse()
    {
        Dialogue dialogue = Dialogues.FindAll(d => d.Relationship == CharacterDefinition.Relationship)[0];

        DialogueManager dialogueManager = FindAnyObjectByType<DialogueManager>();

        if (dialogueManager != null)
        {
            dialogueManager.Begin(dialogue);
        }
    }

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
