using Codice.CM.Common.Mount;
using UnityEditor;
using UnityEngine;

public class Connection
{
    public Node StartingNode;
    public Node EndingNode;

    public Connection(Node startingNode, Node endingNode)
    {
        StartingNode = startingNode;
        EndingNode = endingNode;
    }

    public void Draw()
    {
        Vector2 StartingCenter = StartingNode.Rect.center;
        Vector2 EndingCenter = EndingNode.Rect.center;

        Handles.DrawBezier(StartingCenter, EndingCenter, StartingCenter + Vector2.left * 50f, EndingCenter - Vector2.left * 50f, Color.white, null, 2f);
    }
}
