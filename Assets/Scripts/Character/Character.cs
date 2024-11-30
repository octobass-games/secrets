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
        var tidbits = CharacterDefinition.Tidbits.Select(t => Instantiate(t)).ToList();
        CharacterDefinition = Instantiate(CharacterDefinition);

        Interactions = CharacterDefinition.Interactions;
        CharacterDefinition.Tidbits = tidbits;

        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        EventManager.Instance.Subscribe(GameEventType.INTERACTION_ADVANCE, OnInteractionAdvance);
        EventManager.Instance.Subscribe(GameEventType.CHARACTER_TIDBIT_UNLOCKED, OnCharacterTidbitUnlocked);
    }

    void OnDisable()
    {
        EventManager.Instance.Unsubscribe(GameEventType.INTERACTION_ADVANCE, OnInteractionAdvance);
        EventManager.Instance.Unsubscribe(GameEventType.CHARACTER_TIDBIT_UNLOCKED, OnCharacterTidbitUnlocked);
    }

    public void BeginInteraction()
    {
        CurrentInteraction = Interactions.FindAll(i => i.RelationshipRequirement == CharacterDefinition.Relationship)[0];

        DialogueManager dialogueManager = FindAnyObjectByType<DialogueManager>();

        if (dialogueManager != null)
        {
            dialogueManager.Begin(CurrentInteraction.RootLines[CurrentInteractionDialogueIndex], SpeakerAnimator, null);
        }
    }

    public void EndInteraction()
    {
        CharacterDefinition.Relationship++;
        CurrentInteraction = null;
        CurrentInteractionDialogueIndex = 0;
    }

    public void Load(SaveData saveData)
    {
        CharacterData characterData = saveData.Characters.Find(c => c.Name == CharacterDefinition.Name);

        if (characterData != null)
        {
            CharacterDefinition.Relationship = characterData.Relationship;
            CharacterDefinition.Tidbits.ForEach(t =>
            {
                var savedTidbit = characterData.CharacterTidbits.Find(td => t.Id == td.Id);

                if (savedTidbit != null)
                {
                    t.IsUnlocked = savedTidbit.IsUnlocked;
                }
            });
        }
    }

    public void Save(SaveData saveData)
    {
        List<CharacterTidbitData> characterTidbitsData = CharacterDefinition.Tidbits.Select(t => new CharacterTidbitData(t.Id, t.IsUnlocked)).ToList();

        CharacterData characterData = new(CharacterDefinition.Name, CharacterDefinition.Relationship, characterTidbitsData);

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
            EndInteraction();
        }
    }

    public void OnCharacterTidbitUnlocked(GameEvent @event)
    {
        var tidbit = CharacterDefinition.Tidbits.Find(t => t.IsEqual(@event.CharacterTidbit));

        if (tidbit != null)
        {
            tidbit.IsUnlocked = true;
        }
        else
        {
            Debug.LogError("Attempted to unlock tidbit but could not find it");
        }
    }

    public List<CharacterTidbit> GetUnlockedTidbits()
    {
        return CharacterDefinition.Tidbits.FindAll(tidbit => tidbit.IsUnlocked);
    }
}
