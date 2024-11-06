using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Book : MonoBehaviour, Savable
{
    public BookDefinition BookDefinition;

    // items should have stock as well (maybe)
    public void InsertIntoBook(BookDefinition book, GameObject item)
    {
    }

    // might not be needed
    public void RemoveItemFrom(BookData book, GameObject item)
    {

    }

    public void SellBook(BookDefinition book)
    {
        Debug.Log("Selling: " + book.Name);
    }

    public void OrderStock(BookDefinition book, int amount)
    {
        int totalCost = book.CostToOrder * amount;
        Debug.Log("Ordering stock for: " + totalCost);
    }

    public void SetPrice(int price)
    {
        BookDefinition.SellPrice = price;
    }

    public void Save(SaveData saveData)
    {
        saveData.Books.Add(new BookData(BookDefinition.Name, BookDefinition.SellPrice, BookDefinition.Stock));
    }

    public void Load(SaveData saveData)
    {
        BookData bookData = saveData.Books.Find(b => b.Name == BookDefinition.Name);

        BookDefinition.SellPrice = bookData.SellPrice;
        BookDefinition.Stock = bookData.Stock;
    }
}
