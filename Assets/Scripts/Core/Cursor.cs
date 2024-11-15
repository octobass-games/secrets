using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cursor : MonoBehaviour
{
    public Texture2D NeutralCursor;
    public Texture2D ClickableCursor;
    
    private Vector2 CursorHotspot = Vector2.zero;
    private List<RaycastResult> UiObjectsUnderCursor = new();

    void Awake()
    {
        SetNeutralCursor();
    }

    void Update()
    {
        if (IsClickableBeneathCursor())
        {
            SetClickableCursor();
        }
        else
        {
            SetNeutralCursor();
        }
    }

    private bool IsClickableBeneathCursor()
    {
        var cursorPosition = Input.mousePosition;

        if (EventSystem.current.IsPointerOverGameObject())
        {
            UiObjectsUnderCursor.Clear();
            
            PointerEventData @event = new(EventSystem.current)
            {
                position = cursorPosition
            };

            EventSystem.current.RaycastAll(@event, UiObjectsUnderCursor);

            return UiObjectsUnderCursor.Any(result => result.gameObject.GetComponent<Button>() != null);
        }
        else if (Camera.main != null)
        {
            var ray = Camera.main.ScreenPointToRay(cursorPosition);
            var hit = Physics2D.Raycast(ray.origin, ray.direction);

            return hit && hit.collider.GetComponent<Clickable>() != null;
        }

        return false;
    }

    private void SetNeutralCursor()
    {
        SetCursor(NeutralCursor);
    }

    private void SetClickableCursor()
    {
        SetCursor(ClickableCursor);
    }

    private void SetCursor(Texture2D cursor)
    {
        UnityEngine.Cursor.SetCursor(cursor, CursorHotspot, CursorMode.Auto);
    }
}
