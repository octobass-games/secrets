using UnityEngine;
using UnityEngine.Events;

public class EventResponder : MonoBehaviour, EventSubscriber
{
    public GameEventType EventType;
    public UnityEvent UnityEvent;
    void Awake()
    {
        EventManager.Instance.Subscribe(EventType, this);
    }
    
    public void OnReceive(GameEvent @event)
    {
        UnityEvent.Invoke();
    }
}
