using System.Collections.Generic;
using UnityEngine;

public delegate void EventHandler(GameEvent gameEvent);

public class EventManager : MonoBehaviour
{
    public static EventManager Instance {  get; private set; }

    private Dictionary<GameEventType, List<EventHandler>> EventSubscribers = new();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void Subscribe(GameEventType eventType, EventHandler receiver)
    {
        if (Instance.EventSubscribers.ContainsKey(eventType))
        {
            Instance.EventSubscribers[eventType].Add(receiver);
        }
        else
        {
            Instance.EventSubscribers.Add(eventType, new() { receiver });
        }
    }

    public void Unsubscribe(GameEventType eventType, EventHandler receiver)
    {
        if (Instance.EventSubscribers.ContainsKey(eventType))
        {
            Instance.EventSubscribers[eventType].Remove(receiver);
        }
    }

    public void Publish(GameEvent gameEvent)
    {
        GameEventType eventName = gameEvent.Type;

        if (Instance.EventSubscribers.ContainsKey(eventName))
        {
            // loop backwards to handle one-shot events where the subscriber will
            // unsubscribe in their OnReceive
            for (int i = Instance.EventSubscribers[eventName].Count - 1; i >= 0; i--)
            {
                Instance.EventSubscribers[eventName][i](gameEvent);
            }
        }
    }
}
