using System;
using System.Collections.Generic;

[Serializable]
public class Memory
{
    public List<string> Events;

    public Memory(List<string> events)
    {
        Events = events;
    }
}
