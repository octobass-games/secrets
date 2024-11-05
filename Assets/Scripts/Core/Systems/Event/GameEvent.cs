using System;

[Serializable]
public class GameEvent
{
    public string Type;
    public string Memory;
    public string Paper;

    public GameEvent(string type, string memory, string paper)
    {
        Type = type;
        Memory = memory;
        Paper = paper;
    }
}
