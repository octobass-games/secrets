using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DayManager : MonoBehaviour, Savable
{
    public bool SaveOnDayEnd = false;

    [SerializeField]
    private List<DayDefinition> Days;
    private DayDefinition Today;
    private int TodayEventIndex;
    public Interaction SignInteraction;

    void Awake()
    {
        Days = Days.Select(d => Instantiate(d)).ToList();
    }

    void OnEnable()
    {
        EventManager.Instance.Subscribe(GameEventType.NEXT_DAILY_EVENT, OnNextDailyEvent);
    }

    void OnDisable()
    {
        EventManager.Instance.Unsubscribe(GameEventType.NEXT_DAILY_EVENT, OnNextDailyEvent);
    }

    void Start()
    {
        BeginNextDay();
    }

    public void SkipToDay(int day)
    {
       for (int i = 0; i < Days.Count; i++)
        {
            var dayToUpdate = Days[i];
            dayToUpdate.IsInThePast = i < (day - 1);
        }
        BeginNextDay();
    }

    private void BeginNextDay()
    {
        TodayEventIndex = -1;

        Today = Days.Find(d => !d.IsInThePast);
        
        PublishNextDailyEvent();
    }

    private void EndDay()
    {
        Today.IsInThePast = true;

        if (SaveOnDayEnd)
        {
            SaveManager.Instance.Save();
        }

        BeginNextDay();
    }

    private void OnNextDailyEvent(GameEvent @event)
    {
        PublishNextDailyEvent();
    }

    public void PublishNextDailyEvent()
    {
        TodayEventIndex++;

        if (TodayEventIndex < Today.DailyEvents.Count)
        {
            EventManager.Instance.Publish(Today.DailyEvents[TodayEventIndex]);
        }
        else
        {
            EndDay();
        }
    }

    public void TryFlipSign()
    {
        int nextEventIndex = TodayEventIndex + 1;

        if (nextEventIndex < Today.DailyEvents.Count)
        {
            var nextEvent = Today.DailyEvents[TodayEventIndex + 1];

            if (nextEvent.Type == GameEventType.OPEN_SHOP || nextEvent.Type == GameEventType.CLOSE_SHOP)
            {
                PublishNextDailyEvent();
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
        else
        {
            Debug.LogWarning("Attempted to fetch next daily event but none could be found");
        }
    }

    public Line GetSupplierDialogue()
    {
        return Today.SupplierDialogue;
    }

    public DayDefinition TryGetDayBefore(DayDefinition day)
    {
        var dayIndex = Days.FindIndex(d => d.IsEqual(day));

        if (dayIndex <= 0)
        {
            return null;
        }
        else
        {
            return Days[dayIndex - 1];
        }
    }

    public void Load(SaveData saveData)
    {
        foreach (DayData day in saveData.Days)
        {
            DayDefinition dayDefinition = Days.Find(d => d.Date == day.Date);

            dayDefinition.IsInThePast = day.IsInPast;
        }
    }

    public void Save(SaveData saveData)
    {
        List<DayData> days = Days.Select(d => new DayData(d.Date, d.IsInThePast)).ToList();

        saveData.Days = days;
    }

    public bool IsNextEventType(GameEventType eventType)
    {
      return Today.DailyEvents[TodayEventIndex + 1].Type == eventType;
    }
}
