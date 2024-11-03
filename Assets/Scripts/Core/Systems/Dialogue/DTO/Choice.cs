using System;

[Serializable]
public class Choice
{
    public string Text;
    public string Event;
    public int RelationshipPoints;
    public string NextLineId;
    public string[] Requirements;

    public Choice(string text, string @event, int relationshipPoints, string nextLineId, string[] requirements)
    {
        Text = text;
        Event = @event;
        RelationshipPoints = relationshipPoints;
        NextLineId = nextLineId;
        Requirements = requirements;
    }
}
