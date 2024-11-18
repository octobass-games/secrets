using System;
using System.Collections.Generic;

[Serializable]
public class DailyTransactions
{
    public string Date;
    public List<UniqueBookSale> UniqueBookSales;
    public List<BookSale> BookSales;
    public List<BookOrder> BookOrders;

    public DailyTransactions(string date, List<UniqueBookSale> uniqueBookSales, List<BookSale> bookSales, List<BookOrder> bookOrders)
    {
        Date = date;
        UniqueBookSales = uniqueBookSales;
        BookSales = bookSales;
        BookOrders = bookOrders;
    }
}
