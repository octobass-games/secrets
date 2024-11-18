using System;
using System.Collections.Generic;

[Serializable]
public class BookkeeperData
{
    public int BankBalance;
    public List<BookData> Books;
    public List<DailyTransactions> DailyTransactions;

    public BookkeeperData(int bankBalance, List<BookData> books, List<DailyTransactions> dailyTransactions)
    {
        BankBalance = bankBalance;
        Books = books;
        DailyTransactions = dailyTransactions;
    }
}
