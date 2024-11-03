using System;
using System.Collections.Generic;

[Serializable]
public class Dialogue
{
    public int Relationship;
    public List<Line> Lines;

    public Dialogue(int relationship, List<Line> lines)
    {
        Relationship = relationship;
        Lines = lines;
    }
}
