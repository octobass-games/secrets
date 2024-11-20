using UnityEngine;

public class Book : MonoBehaviour
{
    public BookDefinition BookDefinition;

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
}
