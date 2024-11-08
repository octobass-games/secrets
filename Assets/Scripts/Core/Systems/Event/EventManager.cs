using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public Dictionary<GameEventType, List<EventSubscriber>> EventSubscribers = new();

    public void Subscribe(GameEventType eventType, EventSubscriber receiver)
    {
        if (EventSubscribers.ContainsKey(eventType))
        {
            EventSubscribers[eventType].Add(receiver);
        }
        else
        {
            EventSubscribers.Add(eventType, new() { receiver });
        }
    }

    public void Unsubscribe(GameEventType eventType, EventSubscriber receiver)
    {
        if (EventSubscribers.ContainsKey(eventType))
        {
            EventSubscribers[eventType].Remove(receiver);
        }
    }

    public void Publish(GameEvent gameEvent)
    {
        GameEventType eventName = gameEvent.Type;

        if (EventSubscribers.ContainsKey(eventName))
        {
            // loop backwards to handle one-shot events where the subscriber will
            // unsubscribe in their OnReceive
            for (int i = EventSubscribers[eventName].Count - 1; i >= 0; i--)
            {
                EventSubscribers[eventName][i].OnReceive(gameEvent);
            }
        }
    }
}
