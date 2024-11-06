using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public string Day;
    public List<BookData> Books;
    public AccountData Account;
    public HistoryData History;
    public List<CharacterData> Characters;
    public List<DayData> Days;

    public SaveData(string day, List<BookData> books, AccountData account, HistoryData history, List<CharacterData> characters, List<DayData> days)
    {
        Day = day;
        Books = books;
        Account = account;
        History = history;
        Characters = characters;
        Days = days;
    }

    public SaveData()
    {
        Books = new List<BookData>();
        Characters = new List<CharacterData>();
    }
}
