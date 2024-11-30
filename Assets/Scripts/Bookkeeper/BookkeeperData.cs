using System;
using System.Collections.Generic;

[Serializable]
public class BookkeeperData
{
    public int BankBalance;
    public List<BookData> Books;
    public List<ItemData> Items;
    public List<BookData> HollowBooks;
    public List<DailyTransactions> DailyTransactions;
    public int MonthlyRent;

    public BookkeeperData(int bankBalance, List<BookData> books, List<ItemData> items, List<BookData> hollowBooks, List<DailyTransactions> dailyTransactions, int monthlyRent)
    {
        BankBalance = bankBalance;
        Books = books;
        Items = items;
        HollowBooks = hollowBooks;
        DailyTransactions = dailyTransactions;
        MonthlyRent = monthlyRent;
    }
}
