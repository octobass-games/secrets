using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class FirstDayResponder : MonoBehaviour
{
    public Animator Sign;
    public DayDefinition DayOne;
    public DayManager DayManager;
    
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
        if (@event.Day.IsEqual(DayOne))
        {
            // Start shop open on day one
            Sign.SetTrigger("OpenNoAnimation");
        }
    }
}
