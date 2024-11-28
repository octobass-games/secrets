using System;
using System.Collections.Generic;

[Serializable]
public class BookkeeperData
{
    public int BankBalance;
    public List<BookData> Books;
    public List<BookData> HollowBooks;
    public List<DailyTransactions> DailyTransactions;
    public int MonthlyRent;

    public BookkeeperData(int bankBalance, List<BookData> books, List<BookData> hollowBooks, List<DailyTransactions> dailyTransactions, int monthlyRent)
    {
        BankBalance = bankBalance;
        Books = books;
        HollowBooks = hollowBooks;
        DailyTransactions = dailyTransactions;
        MonthlyRent = monthlyRent;
    }
}
