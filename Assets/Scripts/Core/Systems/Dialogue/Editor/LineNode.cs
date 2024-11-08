using System;
using UnityEditor;
using UnityEngine;

public class LineNode : Node
{
    public Line Line;

    public LineNode(Vector2 position, int width, int height, Action<Node> onRemove) : base(position, width, height, onRemove)
    {
    }

    protected override void CreateScriptableObject()
    {
        Line = ScriptableObject.CreateInstance<Line>();
    }

    protected override void DrawScriptableObject()
    {
        Line.Text = GUI.TextArea(new Rect(Rect.x + 5, Rect.y + 70, Rect.width, (Rect.width / 5) * 3), Line.Text);
    }

    protected override void SaveScriptableObject()
    {
        AssetDatabase.SaveAssets();
    }
}
