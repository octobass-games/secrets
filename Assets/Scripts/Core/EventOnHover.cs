using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EventOnHover : MonoBehaviour
{
    public UnityEvent OnHoverIn;
    public UnityEvent OnHoverOut;

    void OnMouseEnter()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            OnHoverIn?.Invoke();
        }
    }


    void OnMouseExit()
    {
        OnHoverOut?.Invoke();
    }
}
