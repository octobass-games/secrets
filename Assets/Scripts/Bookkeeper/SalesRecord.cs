using System;
using System.Collections.Generic;

[Serializable]
public class SalesRecord
{
    public string Date;
    public List<BookSale> BookSales;

    public SalesRecord(string date, List<BookSale> bookSales)
    {
        Date = date;
        BookSales = bookSales;
    }
}
