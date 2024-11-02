using System;

[Serializable]
public class Choice
{
    public string Text;
    public string Event;
    public string LineId;

    public Choice(string text, string @event, string lineId)
    {
        Text = text;
        Event = @event;
        LineId = lineId;
    }
}
