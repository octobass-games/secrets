using System.Collections.Generic;
using UnityEngine;

public class Line : ScriptableObject
{
    public string Id;
    public string Speaker;
    [TextArea]
    public string Text;
    public Line NextLine;
    public List<GameEvent> Events;
    public List<Choice> Choices;
}
