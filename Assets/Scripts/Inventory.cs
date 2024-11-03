using System;
using System.Collections.Generic;

[Serializable]
public class Inventory
{
    public List<Book> books;

    public Inventory(List<Book> books)
    {
        this.books = books;
    }
}
