using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ConfrontationManager : MonoBehaviour
{
    public GameObject PickerPanel;
    public GameObject ConfrontationBook;
    public Character Maple;

    public Character Essie;
    public Character Gem;
    public Character Ivy;
    public Character HoodedFigue;
    public Character Lera;

    private Character PickedCharacter;
    private List<CharacterTidbit> ExpectedTidbits;
    private int ConfrontationIndex = 0;

    void OnEnable()
    {
        EventManager.Instance.Subscribe(GameEventType.CONFRONT_PICKED_CHARACTER, OnEvent);
        EventManager.Instance.Subscribe(GameEventType.CONFRONT_ASK_FOR_TIDBIT, ConfrontAskForTidbit);
    }

    void OnDisable()
    {
        EventManager.Instance.Unsubscribe(GameEventType.CONFRONT_PICKED_CHARACTER, OnEvent);
        EventManager.Instance.Unsubscribe(GameEventType.CONFRONT_ASK_FOR_TIDBIT, ConfrontAskForTidbit);
    }

    public void OnEvent(GameEvent @event)
    {
        Confront();
    }

    public void ConfrontAskForTidbit(GameEvent @event)
    {
        ExpectedTidbits = @event.CharacterTidbits;
    }

    public void PickEssie()
    {
        PickerPanel.SetActive(false);
        Maple.BeginInteraction();
        PickedCharacter = Essie;
    }

    public void PickLera()
    {
        PickerPanel.SetActive(false);
        Maple.BeginInteraction();
        PickedCharacter = Lera;
    }

    public void PickGem()
    {
        PickerPanel.SetActive(false);
        Maple.BeginInteraction();
        PickedCharacter = Gem;
    }

    public void PickIvy()
    {
        PickerPanel.SetActive(false);
        Maple.BeginInteraction();
        PickedCharacter = Ivy;

    }
    public void PickHoodedFigure()
    {
        PickerPanel.SetActive(false);
        Maple.BeginInteraction();
        PickedCharacter = HoodedFigue;
    }

    public void Confront()
    {
        EventManager.Instance.Publish(new GameEvent() { Type = GameEventType.VISITOR_ARRIVAL, Character = PickedCharacter.CharacterDefinition });
        ConfrontationIndex = 0;
        var clickable = PickedCharacter.GetComponentInChildren<Clickable>();
        PickedCharacter.InConfrontation = true;
        clickable.OnClick.AddListener(() =>
        {
            PickedCharacter.BeginConfrontation(ConfrontationIndex);
        });
    }

    public void SetLastPickedTidbit(CharacterTidbit characterTidbit)
    {
        ConfrontationBook.gameObject.SetActive(false);

        if (ExpectedTidbits.Any(t => t.Tidbit == characterTidbit.Tidbit))
        {
            ConfrontationIndex += 1;
            PickedCharacter.BeginConfrontation(ConfrontationIndex);
        }
        else
        {
            PickedCharacter.BadChoiceInConfronation();
        }
    }
}
