using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Clickable : MonoBehaviour
{
    public UnityEvent OnClick;

    void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            OnClick?.Invoke();
        }
    }
}
