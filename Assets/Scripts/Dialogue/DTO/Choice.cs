using System;

[Serializable]
public class Choice
{
    public string Text;
    public string Event;
    public string LineId;
    public string[] Requirements;

    public Choice(string text, string @event, string lineId, string[] requirements)
    {
        Text = text;
        Event = @event;
        LineId = lineId;
        Requirements = requirements;
    }
}
