using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EventOnHover : MonoBehaviour
{
    public UnityEvent OnHoverIn;
    public UnityEvent OnHoverOut;

    private bool IsMouseOverlapping;
    private bool HasHoverInTriggered;

    void Update()
    {
        if (IsMouseOverlapping && !EventSystem.current.IsPointerOverGameObject() && !HasHoverInTriggered)
        {
            OnHoverIn?.Invoke();
            HasHoverInTriggered = true;
        }
        else if (!IsMouseOverlapping && HasHoverInTriggered)
        {
            OnHoverOut?.Invoke();
            HasHoverInTriggered = false;
        }
    }

    void OnMouseEnter()
    {
        IsMouseOverlapping = true;
    }


    void OnMouseExit()
    {
        IsMouseOverlapping = false;
    }

    void OnDisable()
    {
        OnHoverOut?.Invoke();
    }
}
