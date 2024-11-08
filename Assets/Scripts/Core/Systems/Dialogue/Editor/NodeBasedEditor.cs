using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NodeBasedEditor : EditorWindow
{
    private List<Node> Nodes = new();
    private List<Connection> Connections = new();

    private Node ConnectionStart;
    private GUIStyle NodeStyle;

    [MenuItem("Window/Dialogue editor")]
    public static void ShowEditor()
    {
        EditorWindow window = GetWindow<NodeBasedEditor>();
        window.titleContent = new GUIContent("Dialogue Editor");
    }

    private void OnEnable()
    {
        NodeStyle = new GUIStyle();
        NodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
        NodeStyle.border = new RectOffset(12, 12, 12, 12);
    }

    public void OnGUI()
    {
        Event current = Event.current;

        if (current.type == EventType.MouseDrag)
        {
            foreach (var node in Nodes)
            {
                node.OnDrag(current.delta);
            }
        }

        if (GUILayout.Button("Save"))
        {
            foreach (Node node in Nodes)
            {
                node.ApplyModifications();
            }

            foreach (Node node in Nodes)
            {
                node.ProcessConnections(Connections);
            }

            foreach (Node node in Nodes)
            {
                node.SaveScriptableObject();
            }
        }

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

        foreach(Connection connection in Connections)
        {
            connection.Draw();
        }
    }

    private void AddLineNode(Vector2 position)
    {
        Nodes.Add(new LineNode(position, 250, 250, OnRemoveNode, BeginConnection, EndConnection));
    }

    private void AddChoiceNode(Vector2 position)
    {
        Nodes.Add(new ChoiceNode(position, 250, 250, OnRemoveNode, BeginConnection, EndConnection));
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
        if (ConnectionStart != null)
        {
            Connections.Add(new Connection(ConnectionStart, end));
        }
     
        ConnectionStart = null;
    }
}
