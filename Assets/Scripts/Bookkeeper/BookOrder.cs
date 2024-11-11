using System;

[Serializable]
public class BookOrder
{
    public string Name;
    public int Quantity;

    public BookOrder(string name, int quantity)
    {
        Name = name;
        Quantity = quantity;
    }
}
