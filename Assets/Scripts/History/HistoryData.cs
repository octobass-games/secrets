using System;
using System.Collections.Generic;

[Serializable]
public class HistoryData
{
    public List<string> Events;

    public HistoryData(List<string> events)
    {
        Events = events;
    }
}
