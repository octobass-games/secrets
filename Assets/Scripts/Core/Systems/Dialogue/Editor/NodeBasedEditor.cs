using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class NodeBasedEditor : EditorWindow
{
    private List<Node> Nodes = new();
    private List<Connection> Connections = new();

    private Node ConnectionStart;
    private string SaveDirectoryPath;

    private string TextImportString;
    private CharacterDefinition DefaultSpeaker;
    private bool ShowTextImportString;
    private Line RootLine;
    private Vector2 nextLoadedNodePosition = Vector2.zero;

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
                    node.SaveScriptableObject(relativePathToSaveDirectory);
                }

                foreach (Node node in Nodes)
                {
                    node.ProcessConnections(Connections);
                    node.SaveScriptableObject(relativePathToSaveDirectory);
                }

                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            else
            {
                Debug.LogWarning("Directory path must be provided");
            }
        }

        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();

        RootLine = (Line)EditorGUILayout.ObjectField(RootLine, typeof(Line), false);

        if (GUILayout.Button("Load"))
        {
            if (RootLine != null)
            {
                LoadLine(RootLine);
                LoadConnections();
            }
            else
            {
                Debug.LogWarning("Must specify a dialogue directory to load from");
            }
        }

        DefaultSpeaker = EditorGUILayout.ObjectField("DefaultSpeaker", DefaultSpeaker, typeof(CharacterDefinition), true) as CharacterDefinition;

        ShowTextImportString = EditorGUILayout.Foldout(ShowTextImportString, "Text to import");
        if (ShowTextImportString)
        {
            EditorStyles.textField.wordWrap = true;
            TextImportString = EditorGUILayout.TextArea(TextImportString, GUILayout.Height(100));
        }

        if (GUILayout.Button("Import Text String"))
        {
            var formattedList = TextImportString.Split('\n').Select(s => s.Trim()).Where(s => s != "");
            foreach (string text in formattedList)
            {
                if (text.StartsWith("C:"))
                {
                    Choice newChoice = ScriptableObject.CreateInstance<Choice>();
                    newChoice.Text = text.Replace("C:", "").Trim();
                    AddChoiceNode(new Vector2(nextLoadedNodePosition.x, 350), newChoice);
                    nextLoadedNodePosition += new Vector2(300, 0);
                }
                else
                {
                    Line newLine = ScriptableObject.CreateInstance<Line>();
                    newLine.Text = text;
                    newLine.Speaker = DefaultSpeaker;
                    AddLineNode(nextLoadedNodePosition, newLine);
                    nextLoadedNodePosition += new Vector2(300, 0);
                }
            }
        }

        GUILayout.EndHorizontal();


        if (current != null && current.type == EventType.ContextClick)
        {
            GenericMenu menu = new GenericMenu();

            menu.AddItem(new GUIContent("Add Line"), false, () => AddLineNode(current.mousePosition, null));
            menu.AddItem(new GUIContent("Add Choice"), false, () => AddChoiceNode(current.mousePosition, null));

            menu.ShowAsContext();
        }

        foreach (Node node in Nodes)
        {
            node.ProcessEvents(current);
            node.Draw();
        }

        foreach (Connection connection in Connections)
        {
            connection.Draw();
        }

        if (Event.current.type == EventType.MouseDrag)
        {
            foreach (var node in Nodes)
            {
                node.OnDrag(current.delta);
            }

            GUI.changed = true;
        }
    }

    private Node AddLineNode(Vector2 position, ScriptableObject baseScriptableObject)
    {
        Node node = new LineNode(position, 250, 250, OnRemoveNode, BeginConnection, EndConnection, baseScriptableObject);

        Nodes.Add(node);

        return node;
    }

    private Node AddChoiceNode(Vector2 position, ScriptableObject baseScriptableObject)
    {
        Node node = new ChoiceNode(position, 250, 250, OnRemoveNode, BeginConnection, EndConnection, baseScriptableObject);

        Nodes.Add(node);

        return node;
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

    private void LoadLine(Line line)
    {
        if (Nodes.Find(n => n is LineNode ? ((LineNode)n).Line == line : false) == null)
        {
            AddLineNode(nextLoadedNodePosition, line);

            if (line.NextLine != null)
            {
                nextLoadedNodePosition += new Vector2(250, 0);
                LoadLine(line.NextLine);
            }
            else if (line.Choices != null)
            {
                nextLoadedNodePosition += new Vector2(250, 0);

                foreach (var choice in line.Choices)
                {
                    LoadChoice(choice);
                }
            }
        }
    }

    private void LoadChoice(Choice choice)
    {
        if (Nodes.Find(n => n is ChoiceNode ? ((ChoiceNode)n).Choice == choice : false) == null)
        {
            AddChoiceNode(nextLoadedNodePosition, choice);

            if (choice.NextLine != null)
            {
                nextLoadedNodePosition += new Vector2(250, 0);
                LoadLine(choice.NextLine);
            }
        }
    }

    private void LoadConnections()
    {
        foreach (Node node in Nodes)
        {
            if (node is LineNode)
            {
                LineNode lineNode = (LineNode)node;

                if (lineNode.Line.NextLine != null)
                {
                    Node nextLine = Nodes.Find(n =>
                    {
                        return n is LineNode && ((LineNode)n).Line == lineNode.Line.NextLine;
                    });

                    BeginConnection(lineNode);
                    EndConnection(nextLine);
                }
                else if (lineNode.Line.Choices != null && lineNode.Line.Choices.Count > 0)
                {
                    foreach (Choice choice in lineNode.Line.Choices)
                    {
                        Node choiceNode = Nodes.Find(n =>
                        {
                            return n is ChoiceNode && ((ChoiceNode)n).Choice == choice;
                        });

                        BeginConnection(lineNode);
                        EndConnection(choiceNode);
                    }
                }
            }
            else
            {
                ChoiceNode choiceNode = (ChoiceNode)node;

                if (choiceNode.Choice.NextLine != null)
                {
                    Node nextLine = Nodes.Find(n =>
                    {
                        return n is LineNode && ((LineNode)n).Line == choiceNode.Choice.NextLine;
                    });

                    BeginConnection(choiceNode);
                    EndConnection(nextLine);
                }
            }
        }
    }
}
