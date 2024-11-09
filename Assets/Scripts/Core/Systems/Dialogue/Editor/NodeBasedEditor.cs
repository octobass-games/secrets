using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class NodeBasedEditor : EditorWindow
{
    private List<Node> Nodes = new();
    private List<Connection> Connections = new();

    private Node ConnectionStart;
    private string SaveDirectoryPath;
    private Line RootLineToLoadFrom;

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

        EditorGUILayout.LabelField("Directory to save in (is prefixed with Assets/Data/Characters/): ");
        SaveDirectoryPath = EditorGUILayout.TextField(SaveDirectoryPath);

        if (GUILayout.Button("Save"))
        {
            if (SaveDirectoryPath?.Length > 0)
            {
                string relativePathToSaveDirectory = "Assets/Data/Characters/" + SaveDirectoryPath;
                string absolutePathToSaveDirectory = Application.dataPath + "/Data/Characters/" + SaveDirectoryPath;

                if (!Directory.Exists(absolutePathToSaveDirectory))
                {
                    Directory.CreateDirectory(absolutePathToSaveDirectory);
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
                    Nodes[i].SaveScriptableObject(relativePathToSaveDirectory, i);
                }
            }
            else
            {
                Debug.LogWarning("Directory path must be provided");
            }
        }

        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();

        RootLineToLoadFrom = (Line)EditorGUILayout.ObjectField("Root Line to load from: ", RootLineToLoadFrom, typeof(Line), false);

        if (GUILayout.Button("Load"))
        {
            if (RootLineToLoadFrom != null)
            {
                AddLineNode(Vector2.zero);

                if (RootLineToLoadFrom.NextLine != null)
                {
                    // set line and load
                }
                else
                {
                    // iterate of choices and load
                }
            }
            else
            {
                Debug.LogWarning("Must specify a root Line to load from");
            }
        }

        GUILayout.EndHorizontal();


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
