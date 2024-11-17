using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DayStarter : MonoBehaviour
{
    public GameObject CalendarContainer;
    public TMP_Text BeforeDate;
    public TMP_Text AfterDate;

    void Start()
    {
        EventManager.Instance.Subscribe(GameEventType.BEGIN_DAY, OnBeginDay);
        EventManager.Instance.Subscribe(GameEventType.END_DAY, OnEndDay);
    }

    private void OnBeginDay(GameEvent @event)
    {
        AfterDate.text = @event.Day.Date;

        CalendarContainer.SetActive(true);
    }
    private void OnEndDay(GameEvent @event)
    {
        BeforeDate.text = AfterDate.text;

        CalendarContainer.SetActive(false);
    }
}
