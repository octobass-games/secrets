using System;
using UnityEngine;

[Serializable]
public class ItemData
{
    public string Name;
    public int Stock;

    public ItemData(string name, int stock)
    {
        Name = name;
        Stock = stock;
    }
}
