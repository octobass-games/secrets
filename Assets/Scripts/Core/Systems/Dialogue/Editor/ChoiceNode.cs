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

        EditorGUILayout.PropertyField(Text);
        EditorGUILayout.PropertyField(Events);
        EditorGUILayout.PropertyField(RelationshipPoints);
        EditorGUILayout.PropertyField(Requirements);

        GUILayout.EndArea();
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

    public override void SaveScriptableObject(string pathToDirectory, int index)
    {
        AssetDatabase.CreateAsset(Choice, pathToDirectory + "/choice-" + index + ".asset");
    }

    public override void ApplyModifications()
    {
        SerializedChoice.ApplyModifiedProperties();
    }
}
