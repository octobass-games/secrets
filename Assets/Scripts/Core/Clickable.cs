using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Clickable : MonoBehaviour
{
    public UnityEvent OnClick;

    private Cursor Cursor;

    void Start()
    {
        Cursor = FindFirstObjectByType<Cursor>();
    }

    void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            OnClick?.Invoke();
        }
    }
}
