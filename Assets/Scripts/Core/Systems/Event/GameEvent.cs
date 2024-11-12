using System;

[Serializable]
public class GameEvent
{
    public GameEventType Type;
    public string Memory;
    public PaperDefinition Paper;
    public BookDefinition BookName;
    public DayDefinition Day;
    public CharacterDefinition Character;
}
