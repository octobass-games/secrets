using UnityEngine;
using UnityEngine.Events;

public class EventResponder : MonoBehaviour
{
    public GameEventType EventType;
    public UnityEvent UnityEvent;

    void OnEnable()
    {
        EventManager.Instance.Subscribe(EventType, OnEvent);
    }

    void OnDisable()
    {
        EventManager.Instance.Unsubscribe(EventType, OnEvent);
    }

    public void OnEvent(GameEvent @event)
    {
        UnityEvent.Invoke();
    }
}
