using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ChoiceNode : Node
{
    public Choice Choice;

    protected SerializedObject SerializedChoice;
    protected SerializedProperty Text;
    protected SerializedProperty Events;
    protected SerializedProperty RelationshipPoints;
    protected SerializedProperty Requirements;

    public ChoiceNode(Vector2 position, int width, int height, Action<Node> onRemove, Action<Node> onConnectionStart, Action<Node> onConnectionEnd) : base(position, width, height, onRemove, onConnectionStart, onConnectionEnd)
    {
        Title = "Choice";
    }

    protected override void CreateScriptableObject()
    {
        Choice = ScriptableObject.CreateInstance<Choice>();
        SerializedChoice = new SerializedObject(Choice);

        Text = SerializedChoice.FindProperty("Text");
        Events = SerializedChoice.FindProperty("Events");
        RelationshipPoints = SerializedChoice.FindProperty("RelationshipPoints");
        Requirements = SerializedChoice.FindProperty("Requirements");
    }

    protected override void DrawScriptableObject()
    {
        GUILayout.BeginArea(new Rect(Rect.x, Rect.y + 15, Rect.width, Rect.height - 15));

        EditorGUILayout.PropertyField(Text);
        EditorGUILayout.PropertyField(Events);
        EditorGUILayout.PropertyField(RelationshipPoints);
        EditorGUILayout.PropertyField(Requirements);

        GUILayout.EndArea();

        if (GUI.Button(new Rect(Rect.x + 5, Rect.y + 140, Rect.width, 20), "Start Connection"))
        {
            OnConnectionStart(this);
        }

        if (GUI.Button(new Rect(Rect.x + 5, Rect.y + 280, Rect.width, 20), "End Connection"))
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
                Choice.NextLine = ((LineNode)connection.EndingNode).Line;
            }
            else if (connection.EndingNode == this)
            {
                Debug.Log("This is an ending node!");
            }
        }
    }

    public override void SaveScriptableObject()
    {
        Debug.Log("Creating");
        AssetDatabase.CreateAsset(Choice, "Assets/Data/Test/Again/Again/asset-" + Choice.Text + ".asset");
    }

    public override void ApplyModifications()
    {
        SerializedChoice.ApplyModifiedProperties();
    }
}
