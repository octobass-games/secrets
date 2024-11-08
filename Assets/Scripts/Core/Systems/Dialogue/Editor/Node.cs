using System;
using UnityEditor;
using UnityEngine;

public abstract class Node
{
    public Rect Rect;

    private Action<Node> OnRemove;

    public Node(Vector2 position, int width, int height, Action<Node> onRemove)
    {
        Rect = new Rect(position.x, position.y, width, height);
        
        OnRemove = onRemove;

        CreateScriptableObject();
    }

    public void Draw()
    {
        DrawScriptableObject();
    }

    public void ProcessEvents(Event e)
    {
        if (Rect.Contains(e.mousePosition))
        {
            if (e.type == EventType.ContextClick)
            {
                DrawContextMenu();
            }
        }
    }

    protected abstract void CreateScriptableObject();
    protected abstract void SaveScriptableObject();
    protected abstract void DrawScriptableObject();

    private void DrawContextMenu()
    {
        GenericMenu menu = new GenericMenu();

        menu.AddItem(new GUIContent("Remove node"), false, () => OnRemove(this));

        menu.ShowAsContext();
    }
}
