using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class NodeBasedEditor : EditorWindow
{
    private List<Node> Nodes = new();
    private List<Connection> Connections = new();

    private Node ConnectionStart;
    private string DirectoryPath;

    [MenuItem("Window/Dialogue editor")]
    public static void ShowEditor()
    {
        EditorWindow window = GetWindow<NodeBasedEditor>();
        window.titleContent = new GUIContent("Dialogue Editor");
    }

    public void OnGUI()
    {
        Event current = Event.current;

        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Directory to save in (is prefixed with Assets/Data/Dialogue/): ");
        DirectoryPath = EditorGUILayout.TextField(DirectoryPath);
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Save"))
        {
            if (DirectoryPath?.Length > 0)
            {
                string relativePathToDirectory = "Assets/Data/Dialogue/" + DirectoryPath;
                string absolutePathToDirectory = Application.dataPath + "/Data/Dialogue/" + DirectoryPath;

                if (!Directory.Exists(absolutePathToDirectory))
                {
                    Directory.CreateDirectory(absolutePathToDirectory);
                }

                foreach (Node node in Nodes)
                {
                    node.ApplyModifications();
                }

                foreach (Node node in Nodes)
                {
                    node.ProcessConnections(Connections);
                }

                for (int i = 0; i < Nodes.Count; i++)
                {
                    Nodes[i].SaveScriptableObject(relativePathToDirectory, i);
                }
            }
            else
            {
                Debug.LogWarning("Directory path must be provided");
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

        if (Event.current.type == EventType.MouseDrag)
        {
            foreach (var node in Nodes)
            {
                node.OnDrag(current.delta);
            }
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
