using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EventOnHover : MonoBehaviour
{
    public UnityEvent OnHoverIn;
    public UnityEvent OnHoverOut;

    void OnMouseEnter()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && enabled)
        {
            OnHoverIn?.Invoke();
        }
    }


    void OnMouseExit()
    {
        if (enabled)
        {
            OnHoverOut?.Invoke();
        }

    }

    void OnDisable()
    {
        OnHoverOut?.Invoke();
    }
}
