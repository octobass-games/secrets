using System;

[Serializable]
public class BookData
{
    public string Name;
    public int SellPrice;
    public int Stock;

    public BookData(string name, int sellPrice, int stock)
    {
        Name = name;
        SellPrice = sellPrice;
        Stock = stock;
    }
}
