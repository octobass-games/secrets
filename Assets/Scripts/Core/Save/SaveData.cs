using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public string Day;
    public List<Book> Books;
    public AccountData Account;
    public Memory Memory;
    public List<CharacterData> Characters;

    public SaveData(string day, List<Book> books, AccountData account, Memory memory, List<CharacterData> characters)
    {
        Day = day;
        Books = books;
        Account = account;
        Memory = memory;
        Characters = characters;
    }

    public SaveData()
    {
        Books = new List<Book>();
        Characters = new List<CharacterData>();
    }
}
