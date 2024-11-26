using System;

[Serializable]
public class BookData
{
    public string Name;
    public int SellPrice;
    public int Stock;
    public string Item;

    public BookData(string name, int sellPrice, int stock, string item)
    {
        Name = name;
        SellPrice = sellPrice;
        Stock = stock;
        Item = item;
    }
}
