using UnityEngine;
using UnityEngine.Events;

public class EventResponder : MonoBehaviour
{
    public GameEventType EventType;
    public UnityEvent UnityEvent;

    void Awake()
    {
        EventManager.Instance.Subscribe(EventType, OnEvent);
    }
    
    public void OnEvent(GameEvent @event)
    {
        UnityEvent.Invoke();
    }
}
