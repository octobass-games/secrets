using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public Dictionary<string, List<EventSubscriber>> EventSubscribers = new();

    public void Subscribe(string eventName, EventSubscriber receiver)
    {
        if (EventSubscribers.ContainsKey(eventName))
        {
            EventSubscribers[eventName].Add(receiver);
        }
        else
        {
            EventSubscribers.Add(eventName, new() { receiver });
        }
    }

    public void Unsubscribe(string eventName, EventSubscriber receiver)
    {
        if (EventSubscribers.ContainsKey(eventName))
        {
            EventSubscribers[eventName].Remove(receiver);
        }
    }

    public void Publish(string eventName)
    {
        if (EventSubscribers.ContainsKey(eventName))
        {
            // loop backwards to handle one-shot events where the subscriber will
            // unsubscribe in their OnReceive
            for (int i = EventSubscribers[eventName].Count - 1; i >= 0; i--)
            {
                EventSubscribers[eventName][i].OnReceive(eventName);
            }
        }
    }
}
