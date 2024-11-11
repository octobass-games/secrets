using System;

[Serializable]
public class GameEvent
{
    public GameEventType Type;
    public string Memory;
    public string Paper;
    public string BookName;
    public DayDefinition Day;
    public CharacterDefinition Character;
}
