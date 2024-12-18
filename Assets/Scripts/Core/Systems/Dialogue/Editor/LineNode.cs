using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class LineNode : Node
{
    public Line Line;

    protected SerializedObject SerializedLine;
    protected SerializedProperty Id;
    protected SerializedProperty SpeakerProperty;
    protected SerializedProperty TextProperty;
    protected SerializedProperty EventsProperty;

    private bool IsRootLine;

    public LineNode(Vector2 position, int width, int height, Action<Node> onRemove, Action<Node> onConnectionStart, Action<Node> onConnectionEnd, ScriptableObject baseScriptableObject) : base(position, width, height, onRemove, onConnectionStart, onConnectionEnd, baseScriptableObject)
    {
        Title = "Line";
    }

    protected override void CreateScriptableObject(ScriptableObject baseScriptableObject)
    {
        Line = baseScriptableObject != null ? (Line)baseScriptableObject : ScriptableObject.CreateInstance<Line>();
        SerializedLine = new SerializedObject(Line);

        Id = SerializedLine.FindProperty("Id");

        if (Id.stringValue == null || Id.stringValue == "")
        {
            Id.stringValue = System.Guid.NewGuid().ToString();
        }

        SpeakerProperty = SerializedLine.FindProperty("Speaker");
        TextProperty = SerializedLine.FindProperty("Text");
        EventsProperty = SerializedLine.FindProperty("Events");
    }

    protected override void DrawScriptableObject()
    {
        GUILayout.BeginArea(new Rect(Rect.x, Rect.y + 15, Rect.width, Rect.height - 15));

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Start Connection"))
        {
            OnConnectionStart(this);
        }

        if (GUILayout.Button("End Connection"))
        {
            OnConnectionEnd(this);
        }
        GUILayout.EndHorizontal();

        EditorGUILayout.PropertyField(SpeakerProperty);
        EditorGUILayout.PropertyField(TextProperty);
        EditorGUILayout.PropertyField(EventsProperty);


        GUILayout.EndArea();

    }

    public override void ProcessConnections(List<Connection> connections)
    {
        Line.Choices.Clear();

        IsRootLine = connections.All(c => c.EndingNode != this);

        foreach (Connection connection in connections)
        {
            if (connection.StartingNode == this)
            {
                if (connection.EndingNode is LineNode)
                {
                    Line newLine = ((LineNode)connection.EndingNode).Line;
                    Line.NextLine = newLine;
                    EditorUtility.SetDirty(Line.NextLine);
                }
                else
                {
                    Choice endingChoice = ((ChoiceNode)connection.EndingNode).Choice;
                    Line.Choices.Add(endingChoice);
                    EditorUtility.SetDirty(endingChoice);
                }
            }
            else if (connection.EndingNode == this)
            {
                Debug.Log("This is an ending node!");
            }
        }

        EditorUtility.SetDirty(Line);
    }

    public override void SaveScriptableObject(string pathToDirectory)
    {
        Debug.Log(pathToDirectory);

        if (AssetDatabase.Contains(Line))
        {
            AssetDatabase.SaveAssets();
        }
        else
        {
            AssetDatabase.CreateAsset(Line, pathToDirectory + (IsRootLine ? "/root" : ("/line-" + Line.Id)) + ".asset");
        }
    }

    public override void ApplyModifications()
    {
        SerializedLine.ApplyModifiedProperties();
    }
}
