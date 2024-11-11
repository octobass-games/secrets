using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public string Day;
    public List<BookData> Books;
    public TillData Account;
    public HistoryData History;
    public List<CharacterData> Characters;
    public List<DayData> Days;
    public BookkeeperData Bookkeeper;
    public int PaperCount;

    public SaveData(string day, List<BookData> books, TillData account, HistoryData history, List<CharacterData> characters, List<DayData> days, BookkeeperData bookkeeper, int paperCount)
    {
        Day = day;
        Books = books;
        Account = account;
        History = history;
        Characters = characters;
        Days = days;
        Bookkeeper = bookkeeper;
        PaperCount = paperCount;
    }

    public SaveData()
    {
        Books = new List<BookData>();
        Characters = new List<CharacterData>();
    }
}
