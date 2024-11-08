using System.Collections.Generic;
using UnityEngine;

public class Choice : ScriptableObject
{
    [TextArea]
    public string Text;
    public List<GameEvent> Events;
    public int RelationshipPoints;
    public Line NextLine;
    public List<Requirement> Requirements;
}
