using UnityEngine;
using UnityEngine.Events;

public class HistoricalAction : MonoBehaviour
{
    public History History;
    public string Event;
    public UnityEvent Action;

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
        if (History.Contains(Event))
        {
            Action?.Invoke();
        }
    }

    private void OnHistoryEvent(GameEvent @event)
    {
        if (@event.Memory == Event)
        {
            Action?.Invoke();
        }
    }
}
