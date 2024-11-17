using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class FirstDayResponder : MonoBehaviour
{
    public Animator Sign;
    public DayDefinition DayOne;
    public DayManager DayManager;
    void Awake()
    {
        EventManager.Instance.Subscribe(GameEventType.BEGIN_DAY, OnBeginDay);
    }
    
    public void OnBeginDay(GameEvent @event)
    {
        if (@event.Day.IsEqual(DayOne))
        {
            // Start shop open on day one
            Sign.SetTrigger("OpenNoAnimation");
            StartCoroutine(WaitForNSecondsThenSendPost());
        }
    }

    IEnumerator WaitForNSecondsThenSendPost()
    {
        yield return new WaitForSeconds(4);

        // Deliver post
        EventManager.Instance.Publish(new GameEvent() { Type = GameEventType.NEXT_DAILY_EVENT });
    }
}
