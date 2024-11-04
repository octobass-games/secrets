using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public string Day;
    public List<Book> Books;
    public AccountData Account;
    public HistoryData History;
    public List<CharacterData> Characters;
    public List<DayData> Days;

    public SaveData(string day, List<Book> books, AccountData account, HistoryData history, List<CharacterData> characters, List<DayData> days)
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
        Books = new List<Book>();
        Characters = new List<CharacterData>();
    }
}
