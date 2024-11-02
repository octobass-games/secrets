using System;

[Serializable]
public class Response
{
    public string Text;
    public string Event;
    public string LineId;

    public Response(string text, string @event, string lineId)
    {
        Text = text;
        Event = @event;
        LineId = lineId;
    }
}
