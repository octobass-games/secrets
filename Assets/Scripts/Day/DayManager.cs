using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DayManager : MonoBehaviour, Savable, EventSubscriber
{
    public List<DayDefinition> DayDefinitions = new();

    private DayDefinition Today;
    private int DailyEventIndex;

    void Awake()
    {
        EventManager.Instance.Subscribe(GameEventType.NEXT_DAILY_EVENT, this);
    }

    void Start()
    {
        DailyEventIndex = 0;
        Today = FindNextDay();
        
        PublishNextDailyEvent();
    }

    public void EndDay()
    {
        Today.IsInThePast = true;

        Today = FindNextDay();
    }

    public void Load(SaveData saveData)
    {
        foreach (DayData day in saveData.Days)
        {
            DayDefinition dayDefinition = DayDefinitions.Find(d => d.Date == day.Date);

            dayDefinition.IsInThePast = day.IsInPast;
        }
    }

    public void Save(SaveData saveData)
    {
        List<DayData> days = DayDefinitions.Select(d => new DayData(d.Date, d.IsInThePast)).ToList();

        saveData.Days = days;
    }

    private DayDefinition FindNextDay()
    {
        return DayDefinitions.Find(d => !d.IsInThePast);
    }

    public void OnReceive(GameEvent @event)
    {
        if (Today.DailyEvents.Count > DailyEventIndex + 1)
        {
            DailyEventIndex++;
            PublishNextDailyEvent();
        }
    }

    public void NextEvent()
    {
        DailyEventIndex++;
        PublishNextDailyEvent();
    }

    private void PublishNextDailyEvent()
    {

        if (Today.DailyEvents.Count > 0 && DailyEventIndex < Today.DailyEvents.Count)
        {
            EventManager.Instance.Publish(Today.DailyEvents[DailyEventIndex]);
        }
    }
}
