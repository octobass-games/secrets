using UnityEngine;

public interface EventSubscriber
{
    public void OnReceive(GameEvent @event);
}
