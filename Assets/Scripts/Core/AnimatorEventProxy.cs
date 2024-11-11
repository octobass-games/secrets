using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class AnimatorEventProxy : MonoBehaviour
{
    public UnityEvent Event;

    public void CallEvent()
    {
        Event.Invoke();
    }
}
