using System.Collections.Generic;
using UnityEngine;

public class HistoryEnabler : MonoBehaviour
{
    public History History;
    public string Event;
    public List<MonoBehaviour> Behaviours;

    void OnEnable()
    {
        EventManager.Instance.Subscribe(GameEventType.HISTORY, OnHistoryEvent);
        
    }

    void OnDisable()
    {
        EventManager.Instance.Unsubscribe(GameEventType.HISTORY, OnHistoryEvent);
    }

    void Start()
    {
        if (History.Contains(Event))
        {
            EnableBehaviours();
        }
        else
        {
            DisableBehaviours();
        }
    }

    private void OnHistoryEvent(GameEvent @event)
    {
        if (@event.Memory == Event)
        {
            EnableBehaviours();
        }
    }

    private void EnableBehaviours()
    {
        UpdateBehaviours(true);
    }

    private void DisableBehaviours()
    {
        UpdateBehaviours(false);
    }

    private void UpdateBehaviours(bool enabled)
    {
        Behaviours.ForEach(b => b.enabled = enabled);
    }
}
