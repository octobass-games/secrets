using System;
using UnityEditor;
using UnityEngine;

public class ChoiceNode : Node
{
    public Choice Choice;

    public ChoiceNode(Vector2 position, int width, int height, Action<Node> onRemove) : base(position, width, height, onRemove)
    {
    }

    protected override void CreateScriptableObject()
    {
        Choice = ScriptableObject.CreateInstance<Choice>();
    }

    protected override void DrawScriptableObject()
    {
        Choice.Text = GUI.TextArea(new Rect(Rect.x + 5, Rect.y + 70, Rect.width, (Rect.width / 5) * 3), Choice.Text);
    }

    protected override void SaveScriptableObject()
    {
        AssetDatabase.SaveAssets();
    }
}
