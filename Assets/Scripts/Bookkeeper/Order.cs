using System;

[Serializable]
public class Order
{
    public string Name;
    public int Quantity;
    public int TotalCost;

    public Order(string name, int quantity, int totalCost)
    {
        Name = name;
        Quantity = quantity;
        TotalCost = totalCost;
    }
}
