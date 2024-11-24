using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Clickable : MonoBehaviour
{
    public UnityEvent OnClick;

    void OnMouseDown()
    {
        if (enabled && !EventSystem.current.IsPointerOverGameObject())
        {
            OnClick?.Invoke();
        }
    }
}
