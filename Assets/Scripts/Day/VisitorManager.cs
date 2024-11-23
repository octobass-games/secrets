using System.Collections.Generic;
using UnityEngine;

public class VisitorManager : MonoBehaviour
{
    public List<Character> Visitors;

    void OnEnable()
    {
        EventManager.Instance.Subscribe(GameEventType.VISITOR_ARRIVAL, OnVisitorArrival);
        EventManager.Instance.Subscribe(GameEventType.VISITOR_DEPARTURE, OnVisitorDeparture);
    }

    void OnDisable()
    {
        EventManager.Instance.Unsubscribe(GameEventType.VISITOR_ARRIVAL, OnVisitorArrival);
        EventManager.Instance.Unsubscribe(GameEventType.VISITOR_DEPARTURE, OnVisitorDeparture);
    }

    void Start()
    {
        Visitors.ForEach(v => v.gameObject.SetActive(false));
    }

    public void OnVisitorArrival(GameEvent @event)
    {
        SetCharacterState(@event.Character, true);

    }

    public void OnVisitorDeparture(GameEvent @event)
    {
        SetCharacterState(@event.Character, false);
    }

    private void SetCharacterState(CharacterDefinition character, bool isActive)
    {
        Character visitor = Visitors.Find(v => v.CharacterDefinition.Name == character.Name);

        visitor.gameObject.SetActive(isActive);
    }
}
