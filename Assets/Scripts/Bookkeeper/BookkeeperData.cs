using System;
using System.Collections.Generic;

[Serializable]
public class BookkeeperData
{
    public int BankBalance;
    public List<BookData> Books;
    public List<SalesRecord> SalesRecords;

    public BookkeeperData(int bankBalance, List<BookData> books, List<SalesRecord> salesRecords)
    {
        BankBalance = bankBalance;
        Books = books;
        SalesRecords = salesRecords;
    }
}
