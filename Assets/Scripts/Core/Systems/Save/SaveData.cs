using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public string Day;
    public HistoryData History;
    public List<CharacterData> Characters;
    public List<DayData> Days;
    public BookkeeperData Bookkeeper;
    public int PaperCount;

    public SaveData(string day, HistoryData history, List<CharacterData> characters, List<DayData> days, BookkeeperData bookkeeper, int paperCount)
    {
        Day = day;
        History = history;
        Characters = characters;
        Days = days;
        Bookkeeper = bookkeeper;
        PaperCount = paperCount;
    }

    public SaveData()
    {
        Characters = new List<CharacterData>();
    }
}
