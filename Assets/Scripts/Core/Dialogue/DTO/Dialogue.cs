using System;
using System.Collections.Generic;

[Serializable]
public class Dialogue
{
    public List<Line> Lines;

    public Dialogue(List<Line> lines)
    {
        Lines = lines;
    }
}
