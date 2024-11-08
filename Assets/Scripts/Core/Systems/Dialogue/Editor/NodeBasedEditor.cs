using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NodeBasedEditor : EditorWindow
{
    private List<Node> Nodes = new();
    private List<Connection> Connections = new();

    private Node ConnectionStart;
    private Node ConnectionEnd;

    [MenuItem("Window/Dialogue editor")]
    public static void ShowEditor()
    {
        EditorWindow window = GetWindow<NodeBasedEditor>();
        window.titleContent = new GUIContent("Dialogue Editor");
    }

    public void OnGUI()
    {
        Event current = Event.current;

        if (current != null && current.type == EventType.ContextClick)
        {
            GenericMenu menu = new GenericMenu();

            menu.AddItem(new GUIContent("Add Line"), false, () => AddLineNode(current.mousePosition));
            menu.AddItem(new GUIContent("Add Choice"), false, () => AddChoiceNode(current.mousePosition));

            menu.ShowAsContext();
        }

        foreach (Node node in Nodes)
        {
            node.ProcessEvents(current);
            node.Draw();
        }
    }

    private void AddLineNode(Vector2 position)
    {
        Nodes.Add(new LineNode(position, 100, 100, OnRemoveNode));
    }

    private void AddChoiceNode(Vector2 position)
    {
        Nodes.Add(new ChoiceNode(position, 100, 100, OnRemoveNode));
    }

    private void OnRemoveNode(Node node)
    {
        Nodes.Remove(node);
    }

    private void BeginConnection(Node connectionStart)
    {
        ConnectionStart = connectionStart;
    }

    private void EndConnection(Node end)
    {
        Connections.Add(new Connection(ConnectionStart, end));

        ConnectionStart = null;
    }
}
