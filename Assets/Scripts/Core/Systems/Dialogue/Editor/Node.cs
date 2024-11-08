using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Node
{
    public  Rect Rect;
    
    protected Action<Node> OnRemove;
    protected Action<Node> OnConnectionStart;
    protected Action<Node> OnConnectionEnd;
    protected string Title;

    public Node(Vector2 position, int width, int height, Action<Node> onRemove, Action<Node> onConnectionStart, Action<Node> onConnectionEnd)
    {
        Rect = new Rect(position.x, position.y, width, height);
        
        OnRemove = onRemove;
        OnConnectionStart = onConnectionStart;
        OnConnectionEnd = onConnectionEnd;

        CreateScriptableObject();
    }

    public void Draw()
    {
        GUI.Box(Rect, Title);

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

    public void OnDrag(Vector2 delta)
    {
        Rect.position += delta;
    }

    public  abstract void ProcessConnections(List<Connection> connections);
    public abstract void SaveScriptableObject();
    public abstract void ApplyModifications();
    
    protected abstract void CreateScriptableObject();
    protected abstract void DrawScriptableObject();

    private void DrawContextMenu()
    {
        GenericMenu menu = new GenericMenu();

        menu.AddItem(new GUIContent("Remove node"), false, () => OnRemove(this));

        menu.ShowAsContext();
    }
}
