using System;
using System.Collections.Generic;

[Serializable]
public class DailyTransactions
{
    public string Date;
    public List<UniqueBookSale> UniqueBookSales;
    public List<BookSale> BookSales;
    public List<BookOrder> BookOrders;
    public int Tax;
    public int Rent;

    public DailyTransactions(string date, List<UniqueBookSale> uniqueBookSales, List<BookSale> bookSales, List<BookOrder> bookOrders, int tax, int rent)
    {
        Date = date;
        UniqueBookSales = uniqueBookSales;
        BookSales = bookSales;
        BookOrders = bookOrders;
        Tax = tax;
        Rent = rent;
    }
}
