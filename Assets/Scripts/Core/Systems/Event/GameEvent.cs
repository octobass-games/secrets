using System;

[Serializable]
public class GameEvent
{
    public string Type;
    public string Memory;
    public string Paper;
    public string BookName;

    public GameEvent(string type, string memory, string paper, string bookName)
    {
        Type = type;
        Memory = memory;
        Paper = paper;
        BookName = bookName;
    }
}
