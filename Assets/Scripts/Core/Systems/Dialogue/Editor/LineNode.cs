using System;
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

    protected override void SaveScriptableObject()
    {
        SerializedLine.ApplyModifiedProperties();

        AssetDatabase.SaveAssets();
    }
}
