using System;
using System.Collections.Generic;

[Serializable]
public class GameEvent
{
    public GameEventType Type;
    public string Memory;
    public PaperDefinition Paper;
    public BookDefinition BookName;
    public DayDefinition Day;
    public CharacterDefinition Character;
    public CharacterTidbit CharacterTidbit;
    public List<CharacterTidbit> CharacterTidbits;
    public int Amount;
    public List<BookDefinition> BooksToOrder;
    public List<ItemDefinition> ItemsToOrder;
    public StampDefinition Stamp;
    public string StampId;
    public bool TriggerNextDailyEvent;
    public bool SellForFree;
    public List<Requirement> Requirements;
    public string Message;
    public List<ItemDefinition> Items;
}
