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
        Vector2 StartingCenter = new Vector2(StartingNode.Rect.xMax, StartingNode.Rect.center.y);
        Vector2 EndingCenter = new Vector2(EndingNode.Rect.xMin, EndingNode.Rect.center.y);

        Handles.DrawBezier(StartingCenter, EndingCenter, StartingCenter + Vector2.right * 50f, EndingCenter - Vector2.right * 50f, Color.white, null, 2f);
    }
}
