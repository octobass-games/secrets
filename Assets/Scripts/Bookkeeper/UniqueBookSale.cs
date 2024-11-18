using System;

[Serializable]
public class UniqueBookSale
{
    public string BuyerName;
    public string BookName;
    public int SellPrice;

    public UniqueBookSale(string buyerName, string bookName, int sellPrice)
    {
        BuyerName = buyerName;
        BookName = bookName;
        SellPrice = sellPrice;
    }
}
