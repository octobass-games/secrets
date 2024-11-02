using System;
using System.Collections.Generic;

[Serializable]
public class Line
{
    public string Id;
    public string Speaker;
    public string Text;
    public string NextLineId;
    public List<Choice> Choices;

    public Line(string id, string speaker, string text, string nextLineId, List<Choice> choices)
    {
        Id = id;
        Speaker = speaker;
        Text = text;
        NextLineId = nextLineId;
        Choices = choices;
    }
}
