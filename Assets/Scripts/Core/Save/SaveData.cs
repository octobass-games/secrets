using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public string Day;
    public List<Book> Books;
    public Account Account;
    public Memory Memory;

    public SaveData(string day, List<Book> books, Account account, Memory memory)
    {
        Day = day;
        Books = books;
        Account = account;
        Memory = memory;
    }

    public SaveData()
    {
        Books = new List<Book>();
    }
}
