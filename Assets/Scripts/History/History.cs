using System.Collections.Generic;
using UnityEngine;

public class History : MonoBehaviour, Savable, EventSubscriber
{
    private List<string> Events = new();

    void Start()
    {
        FindFirstObjectByType<EventManager>().Subscribe(GameEventType.HISTORY, this);
    }

    public void Record(string eventName)
    {
        Debug.Log("Recorded: " + eventName);
        Events.Add(eventName);
    }

    public bool Contains(string eventName)
    {
        return Events.Contains(eventName);
    }

    public void Load(SaveData saveData)
    {
        Events = saveData.History.Events;
    }

    public void Save(SaveData saveData)
    {
        saveData.History= new HistoryData(Events);
    }

    public void OnReceive(GameEvent @event)
    {
        Record(@event.Memory);
    }
}
