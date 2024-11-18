using UnityEngine;

public class Book : MonoBehaviour
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
}
