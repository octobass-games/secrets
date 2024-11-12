using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class FirstDayResponder : MonoBehaviour, EventSubscriber
{
    public Animator Sign;
    public DayDefinition DayOne;
    public DayManager DayManager;
    void Awake()
    {
        EventManager.Instance.Subscribe(GameEventType.BEGIN_DAY, this);
    }
    
    public void OnReceive(GameEvent @event)
    {
        if (@event.Day == DayOne)
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
        DayManager.NextEvent();
    }
}
