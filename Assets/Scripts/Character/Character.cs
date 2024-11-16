using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Character : MonoBehaviour, Savable
{
    public CharacterDefinition CharacterDefinition;
    public Animator SpeakerAnimator;

    private List<Interaction> Interactions;
    private Interaction CurrentInteraction;
    private int CurrentInteractionDialogueIndex;

    void Awake()
    {
        Interactions = CharacterDefinition.Interactions;
    }

    void Start()
    {
        EventManager.Instance.Subscribe(GameEventType.INTERACTION_ADVANCE, OnInteractionAdvance);
        EventManager.Instance.Subscribe(GameEventType.CHARACTER_TIDBIT_UNLOCKED, OnCharacterTidbitUnlocked);
    }

    public void BeginInteraction()
    {
        CurrentInteraction = Interactions.FindAll(i => i.RelationshipRequirement == CharacterDefinition.Relationship)[0];

        DialogueManager dialogueManager = FindAnyObjectByType<DialogueManager>();

        if (dialogueManager != null)
        {
            dialogueManager.Begin(CurrentInteraction.RootLines[CurrentInteractionDialogueIndex], SpeakerAnimator);
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
        CharacterData characterData = new(CharacterDefinition.Name, CharacterDefinition.Relationship, new List<string>());

        saveData.Characters.Add(characterData);
    }

    public void OnInteractionAdvance(GameEvent _)
    {
        if (CurrentInteractionDialogueIndex < CurrentInteraction.RootLines.Count - 1)
        {
            CurrentInteractionDialogueIndex++;
        }
        else
        {
            // Todo:Handle moving to next interaction
        }
    }

    public void OnCharacterTidbitUnlocked(GameEvent @event)
    {
        var tidbit = CharacterDefinition.Tidbits.Find(t => t == @event.CharacterTidbit);

        tidbit.Unlocked = true;
    }
}
