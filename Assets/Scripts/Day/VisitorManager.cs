using System.Collections.Generic;
using UnityEngine;

public class VisitorManager : MonoBehaviour, EventSubscriber
{
    public List<Character> Visitors;

    void Awake()
    {
        EventManager.Instance.Subscribe(GameEventType.VISITOR_ARRIVAL, this);
        EventManager.Instance.Subscribe(GameEventType.VISITOR_DEPARTURE, this);
    }

    void Start()
    {
        Visitors.ForEach(v => v.gameObject.SetActive(false));
    }

    public void OnReceive(GameEvent @event)
    {
        if (@event.Type == GameEventType.VISITOR_ARRIVAL)
        {
            SetCharacterState(@event.Character, true);
        }
        else if (@event.Type == GameEventType.VISITOR_DEPARTURE)
        {
            SetCharacterState(@event.Character, false);
        }
    }

    private void SetCharacterState(CharacterDefinition character, bool isActive)
    {
        Character visitor = Visitors.Find(v => v.CharacterDefinition.Name == character.Name);

        visitor.gameObject.SetActive(isActive);
    }
}
