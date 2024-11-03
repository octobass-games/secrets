using UnityEngine;

public interface EventSubscriber
{
    public void OnReceive(string eventName);
}
