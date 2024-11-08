using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Line : ScriptableObject
{
    public string Id;
    public string Speaker;
    public string Text;
    public string NextLineId;
    public List<GameEvent> Events;
    public List<Choice> Choices;

    public Line(string id, string speaker, string text, string nextLineId, List<GameEvent> events, List<Choice> choices)
    {
        Id = id;
        Speaker = speaker;
        Text = text;
        NextLineId = nextLineId;
        Events = events;
        Choices = choices;
    }
}
