using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Interaction : ScriptableObject
{
    public int RelationshipRequirement;
    public List<Line> RootLines;

    public Interaction(int relationshipRequirement, List<Line> rootLines)
    {
        RelationshipRequirement = relationshipRequirement;
        RootLines = rootLines;
    }
}
