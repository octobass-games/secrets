using UnityEngine;

public class Book : MonoBehaviour, Savable
{
    [SerializeField]
    private BookDefinition BookDefinition;

    void Awake()
    {
        BookDefinition = Instantiate(BookDefinition);
    }

    public bool IsCalled(string name)
    {
        return BookDefinition.Name == name;
    }

    public void InsertIntoBook(GameObject item)
    {
        BookDefinition.Item = item;
    }

    public void RemoveItemFromBook()
    {
        BookDefinition.Item = null;
    }

    public int GetSellPrice()
    {
        return BookDefinition.SellPrice;
    }

    public bool InStock()
    {
        return BookDefinition.Stock > 0;
    }

    public void DecrementStock()
    {
        if (BookDefinition.Stock > 0)
        {
            BookDefinition.Stock--;
        }
    }

    public void OrderStock(int amount)
    {
        BookDefinition.Stock += amount;
    }

    public void IncrementPrice()
    {
        BookDefinition.SellPrice += 1;
    }

    public void DecrementPrice()
    {
        BookDefinition.SellPrice -= 1;
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
