using UnityEngine;
using UnityEngine.Events;

public class FirstDayResponder : MonoBehaviour, EventSubscriber
{
    public Animator Sign;
    public DayDefinition DayOne;
    void Awake()
    {
        EventManager.Instance.Subscribe(GameEventType.BEGIN_DAY, this);
    }
    
    public void OnReceive(GameEvent @event)
    {
        if (@event.Day == DayOne)
        {
            Sign.SetTrigger("OpenNoAnimation");
        }
    }
}
