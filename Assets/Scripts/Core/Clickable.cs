using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Clickable : MonoBehaviour
{
    public UnityEvent OnClick;
    public UnityEvent OnRightClick;

    void OnMouseOver()
    {
        if (enabled && !EventSystem.current.IsPointerOverGameObject())
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
}
