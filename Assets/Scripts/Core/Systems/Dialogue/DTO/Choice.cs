using System;
using System.Collections.Generic;

[Serializable]
public class Choice
{
    public string Text;
    public List<GameEvent> Events;
    public int RelationshipPoints;
    public string NextLineId;
    public List<Requirement> Requirements;

    public Choice(string text, List<GameEvent> events, int relationshipPoints, string nextLineId, List<Requirement> requirements)
    {
        Text = text;
        Events = events;
        RelationshipPoints = relationshipPoints;
        NextLineId = nextLineId;
        Requirements = requirements;
    }
}
