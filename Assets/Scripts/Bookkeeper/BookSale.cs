using System;

[Serializable]
public class BookSale
{
    public string Name;
    public int SellPrice;
    public int Quantity;

    public BookSale(string name, int sellPrice, int quantity)
    {
        Name = name;
        SellPrice = sellPrice;
        Quantity = quantity;
    }
}
