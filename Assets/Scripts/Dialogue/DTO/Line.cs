using System;
using System.Collections.Generic;

[Serializable]
public class Line
{
    public string Id;
    public string Speaker;
    public string Text;
    public List<Response> Responses;

    public Line(string id, string speaker, string text, List<Response> responses)
    {
        Id = id;
        Speaker = speaker;
        Text = text;
        Responses = responses;
    }
}
