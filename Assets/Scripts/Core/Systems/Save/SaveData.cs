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
    public CalendarData Calendar;

    public SaveData(string day, List<Book> books, AccountData account, HistoryData history, List<CharacterData> characters, CalendarData calendar)
    {
        Day = day;
        Books = books;
        Account = account;
        History = history;
        Characters = characters;
        Calendar = calendar;
    }

    public SaveData()
    {
        Books = new List<Book>();
        Characters = new List<CharacterData>();
    }
}
