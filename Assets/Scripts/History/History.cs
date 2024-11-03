using System.Collections.Generic;
using UnityEngine;

public class History : MonoBehaviour, Savable
{
    private List<string> Events = new();

    public void Record(string eventName)
    {
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
        saveData.History.Events = Events;
    }
}
