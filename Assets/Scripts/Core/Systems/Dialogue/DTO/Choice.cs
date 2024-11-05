using System;
using System.Collections.Generic;

[Serializable]
public class Choice
{
    public string Text;
    public List<GameEvent> Events;
    public int RelationshipPoints;
    public string NextLineId;
    public string[] Requirements;

    public Choice(string text, List<GameEvent> events, int relationshipPoints, string nextLineId, string[] requirements)
    {
        Text = text;
        Events = events;
        RelationshipPoints = relationshipPoints;
        NextLineId = nextLineId;
        Requirements = requirements;
    }
}
