using System.Collections.Generic;
using UnityEngine;

public class Choice : ScriptableObject
{
    public string Text;
    public List<GameEvent> Events;
    public int RelationshipPoints;
    public Line NextLine;
    public List<Requirement> Requirements;

    public Choice(string text, List<GameEvent> events, int relationshipPoints, Line nextLine, List<Requirement> requirements)
    {
        Text = text;
        Events = events;
        RelationshipPoints = relationshipPoints;
        NextLine = nextLine;
        Requirements = requirements;
    }
}
