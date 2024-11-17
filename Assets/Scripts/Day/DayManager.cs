using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DayManager : MonoBehaviour, Savable
{
    public List<DayDefinition> DayDefinitions = new();

    private DayDefinition Today;
    private int DailyEventIndex;
    public Interaction SignInteraction;

    void Awake()
    {
        EventManager.Instance.Subscribe(GameEventType.NEXT_DAILY_EVENT, OnNextDailyEvent);
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

    public void OnNextDailyEvent(GameEvent @event)
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

    public void TryFlipSign()
    {
        // TODO: Add check that index is in bounds
        var nextEvent = Today.DailyEvents[DailyEventIndex + 1];

        if (nextEvent.Type == GameEventType.CLOSE_SHOP)
        {
            NextEvent();

        }
        else if (nextEvent.Type == GameEventType.OPEN_SHOP)
        {
            NextEvent();
        }
        else
        {
            DialogueManager dialogueManager = FindAnyObjectByType<DialogueManager>();

            if (dialogueManager != null)
            {
                dialogueManager.Begin(SignInteraction.RootLines[0], null);
            }
        }
    }

    public SupplierInteraction GetSupplierInteraction()
    {
        return Today.SupplierInteraction;
    }
}
