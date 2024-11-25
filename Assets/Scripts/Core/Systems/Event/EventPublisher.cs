using UnityEngine;

public class EventPublisher : MonoBehaviour
{
    public GameEvent GameEvent;

    public void Publish()
    {
        EventManager.Instance.Publish(GameEvent);
    }
}
