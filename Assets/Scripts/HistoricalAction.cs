using UnityEngine;
using UnityEngine.Events;

public class HistoricalAction : MonoBehaviour
{
    public History History;
    public string Event;
    public UnityEvent Action;
    public string UntilEvent;
    public UnityEvent UntilAction;

    void OnEnable()
    {
        EventManager.Instance.Subscribe(GameEventType.HISTORY, OnHistoryEvent);
    }

    void OnDisable()
    {
        EventManager.Instance.Unsubscribe(GameEventType.HISTORY, OnHistoryEvent);
    }

    void Start()
    {
        if (History.Contains(Event) && !History.Contains(UntilEvent))
        {
            Action?.Invoke();
        }
    }

    private void OnHistoryEvent(GameEvent @event)
    {
        if (@event.Memory == Event && !History.Contains(UntilEvent))
        {
            Action?.Invoke();
        }

        if (@event.Memory == UntilEvent)
        {
            UntilAction?.Invoke();
        }
    }
}
