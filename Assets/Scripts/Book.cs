using System;

[Serializable]
public class Book
{
    public string Name;
    public int Price;
    public int Stock;

    public Book(string name, int price, int stock)
    {
        Name = name;
        Price = price;
        Stock = stock;
    }
}
