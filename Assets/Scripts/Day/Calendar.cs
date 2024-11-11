using TMPro;
using UnityEngine;

public class Calendar : MonoBehaviour, EventSubscriber
{
    public TMP_Text View;

    void Awake()
    {
        EventManager.Instance.Subscribe(GameEventType.BEGIN_DAY, this);
    }
    
    public void OnReceive(GameEvent @event)
    {
        if (@event.Day != null)
        {
            View.text = @event.Day.Date;
        }
    }
}
