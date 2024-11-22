using TMPro;
using UnityEngine;

public class Calendar : MonoBehaviour
{
    public TMP_Text View;

    void OnEnable()
    {
        EventManager.Instance.Subscribe(GameEventType.BEGIN_DAY, OnBeginDay);
    }

    void OnDisable()
    {
        EventManager.Instance.Unsubscribe(GameEventType.BEGIN_DAY, OnBeginDay);
    }

    public void OnBeginDay(GameEvent @event)
    {
        if (@event.Day != null)
        {
            View.text = @event.Day.Date;
        }
    }
}
