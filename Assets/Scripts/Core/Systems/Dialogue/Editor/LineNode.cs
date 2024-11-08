using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LineNode : Node
{
    public Line Line;

    protected SerializedObject SerializedLine;
    protected SerializedProperty SpeakerProperty;
    protected SerializedProperty TextProperty;
    protected SerializedProperty EventsProperty;

    public LineNode(Vector2 position, int width, int height, Action<Node> onRemove, Action<Node> onConnectionStart, Action<Node> onConnectionEnd) : base(position, width, height, onRemove, onConnectionStart, onConnectionEnd)
    {
        Title = "Line";
    }

    protected override void CreateScriptableObject()
    {
        Line = ScriptableObject.CreateInstance<Line>();
        SerializedLine = new SerializedObject(Line);

        SpeakerProperty = SerializedLine.FindProperty("Speaker");
        TextProperty = SerializedLine.FindProperty("Text");
        EventsProperty = SerializedLine.FindProperty("Events");
    }

    protected override void DrawScriptableObject()
    {
        GUILayout.BeginArea(new Rect(Rect.x, Rect.y + 15, Rect.width, Rect.height - 15));

        EditorGUILayout.PropertyField(SpeakerProperty);
        EditorGUILayout.PropertyField(TextProperty);
        EditorGUILayout.PropertyField(EventsProperty);

        GUILayout.EndArea();

        if (GUI.Button(new Rect(Rect.x + 5, Rect.y + 140, Rect.width, (Rect.height / 5) * 3), "Start Connection"))
        {
            OnConnectionStart(this);
        }

        if (GUI.Button(new Rect(Rect.x + 5, Rect.y + 280, Rect.width, (Rect.height / 5) * 3), "End Connection"))
        {
            OnConnectionEnd(this);
        }
    }

    public override void ProcessConnections(List<Connection> connections)
    {
        foreach (Connection connection in connections)
        {
            if (connection.StartingNode == this)
            {
                if (connection.EndingNode is LineNode)
                {
                    Line.NextLine = ((LineNode) connection.EndingNode).Line;
                    Debug.Log(Line.NextLine.Text);
                }
                else
                {
                    Line.Choices.Add(((ChoiceNode)connection.EndingNode).Choice);
                }
            }
            else if (connection.EndingNode == this)
            {
                Debug.Log("This is an ending node!");
            }
        }
    }

    public override void SaveScriptableObject()
    {
        SerializedLine.ApplyModifiedProperties();
    }
}
