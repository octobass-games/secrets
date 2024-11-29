using System;
using System.Collections.Generic;

[Serializable]
public class DailyTransactions
{
    public string Date;
    public List<UniqueBookSale> UniqueBookSales;
    public List<BookSale> BookSales;
    public List<Order> BookOrders;
    public List<Order> ItemOrders;
    public int Tax;
    public int Rent;
    public int EndOfDayBankBalance;

    public DailyTransactions(string date, List<UniqueBookSale> uniqueBookSales, List<BookSale> bookSales, List<Order> bookOrders, List<Order> itemOrders, int tax, int rent, int endOfDayBankBalance)
    {
        Date = date;
        UniqueBookSales = uniqueBookSales;
        BookSales = bookSales;
        BookOrders = bookOrders;
        ItemOrders = itemOrders;
        Tax = tax;
        Rent = rent;
        EndOfDayBankBalance = endOfDayBankBalance;
    }
}
