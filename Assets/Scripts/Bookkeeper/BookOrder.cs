using System;

[Serializable]
public class BookOrder
{
    public string Name;
    public int Quantity;
    public int TotalCost;

    public BookOrder(string name, int quantity, int totalCost)
    {
        Name = name;
        Quantity = quantity;
        TotalCost = totalCost;
    }
}
