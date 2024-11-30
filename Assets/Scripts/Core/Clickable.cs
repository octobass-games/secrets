using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Clickable : MonoBehaviour
{
    public UnityEvent OnClick;
    public UnityEvent OnRightClick;

    private bool IsMouseOverlapping;

    void Update()
    {
        if (IsMouseOverlapping && !EventSystem.current.IsPointerOverGameObject())
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                OnClick?.Invoke();
            }
            else if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                OnRightClick?.Invoke();
            }
        }
    }

    void OnMouseEnter()
    {
        if (enabled)
        {
            IsMouseOverlapping = true;
        }
    }

    void OnMouseExit()
    {
        if (enabled)
        {
            IsMouseOverlapping = false;
        }
    }
}
