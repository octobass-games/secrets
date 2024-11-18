using System;
using System.Collections.Generic;

[Serializable]
public class DailyTransactions
{
    public string Date;
    public List<BookSale> BookSales;
    public List<BookOrder> BookOrders;

    public DailyTransactions(string date, List<BookSale> bookSales, List<BookOrder> bookOrders)
    {
        Date = date;
        BookSales = bookSales;
        BookOrders = bookOrders;
    }
}
