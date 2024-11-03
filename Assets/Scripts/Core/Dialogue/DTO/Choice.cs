using System;

[Serializable]
public class Choice
{
    public string Text;
    public string Event;
    public string NextLineId;
    public string[] Requirements;

    public Choice(string text, string @event, string nextLineId, string[] requirements)
    {
        Text = text;
        Event = @event;
        NextLineId = nextLineId;
        Requirements = requirements;
    }
}
