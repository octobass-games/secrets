using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cursor : MonoBehaviour
{
    public Texture2D NeutralCursor;
    public Texture2D ClickableCursor;
    
    private Vector2 CursorHotspot = Vector2.zero;

    void Awake()
    {
        SetNeutralCursor();
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            PointerEventData ped = new PointerEventData(EventSystem.current);
            ped.position = Input.mousePosition;
            List<RaycastResult> results = new();
            
            EventSystem.current.RaycastAll(ped, results);

            bool isButton = false;

            foreach (RaycastResult r in results) {
                if (r.gameObject.GetComponent<Button>() != null)
                {
                    isButton = true;
                }
            }

            if (isButton)
            {
                    SetClickableCursor();
            }
            else
            {
                SetNeutralCursor();
            }
        }
        else if (Camera.main != null)
        {
            var mousePosition = Input.mousePosition;

            var ray = Camera.main.ScreenPointToRay(mousePosition);
            var hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider?.GetComponent<Clickable>() != null)
            {
                SetClickableCursor();
            }
            else
            {
                SetNeutralCursor();
            }
        }
    }

    public void SetNeutralCursor()
    {
        SetCursor(NeutralCursor);
    }

    public void SetClickableCursor()
    {
        SetCursor(ClickableCursor);
    }

    private void SetCursor(Texture2D cursor)
    {
        UnityEngine.Cursor.SetCursor(cursor, CursorHotspot, CursorMode.Auto);
    }
}
