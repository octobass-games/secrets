using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Character : MonoBehaviour, Savable, EventSubscriber
{
    public CharacterDefinition CharacterDefinition;

    private List<Interaction> Interactions;
    private Interaction CurrentInteraction;
    private int CurrentInteractionDialogueIndex;

    void Awake()
    {
        Interactions = CharacterDefinition.Interactions.Select(d => JsonUtility.FromJson<Interaction>(d.text)).ToList();
    }

    void Start()
    {
        FindFirstObjectByType<EventManager>().Subscribe(GameEventType.INTERACTION_ADVANCE, this);
    }

    public void BeginInteraction()
    {
        CurrentInteraction = Interactions.FindAll(i => i.RelationshipRequirement == CharacterDefinition.Relationship)[0];

        DialogueManager dialogueManager = FindAnyObjectByType<DialogueManager>();

        if (dialogueManager != null)
        {
            dialogueManager.Begin(CurrentInteraction.Dialogues[CurrentInteractionDialogueIndex]);
        }
    }

    public void EndInteraction()
    {
        CurrentInteraction = null;
        CurrentInteractionDialogueIndex = 0;
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

    public void OnReceive(GameEvent _)
    {
        if (CurrentInteractionDialogueIndex < CurrentInteraction.Dialogues.Count - 1)
        {
            CurrentInteractionDialogueIndex++;
        }
        else
        {
            // Todo:Handle moving to next interaction
        }
    }
}
