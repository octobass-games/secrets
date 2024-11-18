using TMPro;
using UnityEngine;

public class DayStarter : MonoBehaviour
{
    public DayManager DayManager;
    public GameObject CalendarContainer;
    public TMP_Text BeforeDate;
    public TMP_Text AfterDate;

    void Awake()
    {
        EventManager.Instance.Subscribe(GameEventType.BEGIN_DAY, OnBeginDay);
        EventManager.Instance.Subscribe(GameEventType.END_DAY, OnEndDay);
    }

    private void OnBeginDay(GameEvent @event)
    {
        DayDefinition day = @event.Day;
        DayDefinition dayBefore = DayManager.TryGetDayBefore(day);

        if (dayBefore != null)
        {
            BeforeDate.text = dayBefore.Date;
        }

        AfterDate.text = day.Date;

        CalendarContainer.SetActive(true);
    }
    private void OnEndDay(GameEvent @event)
    {
        BeforeDate.text = AfterDate.text;

        CalendarContainer.SetActive(false);
    }
}
