using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class VisitorManager : MonoBehaviour
{
    public RequirementManager RequirementManager;
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
        if (@event.Requirements != null &&  @event.Requirements.Count > 0)
        {
            if (RequirementManager.AllSatisfied(@event.Requirements))
            {
                SetCharacterOn(@event.Character);
            }
            else
            {
                EventManager.Instance.Publish(new GameEvent() { Type = GameEventType.NEXT_DAILY_EVENT });
            }
        }
        else
        {
            SetCharacterOn(@event.Character);
        }
    }

    public void OnVisitorDeparture(GameEvent @event)
    {
        SetCharacterOff(@event.Character);

        if (@event.TriggerNextDailyEvent)
        {
            EventManager.Instance.Publish(new GameEvent() { Type = GameEventType.NEXT_DAILY_EVENT });
        }
    }

  
    private void SetCharacterOff(CharacterDefinition character)
    {
        Character visitor = Visitors.Find(v => v.CharacterDefinition.Name == character.Name);

        var animator = visitor.gameObject.GetComponentInChildren<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("Leave");
        }
        else
        {
            visitor.gameObject.SetActive(false);
        }
    }

    private void SetCharacterOn(CharacterDefinition character)
    {
        Character visitor = Visitors.Find(v => v.CharacterDefinition.Name == character.Name);
        visitor.gameObject.SetActive(true);
    }
}
