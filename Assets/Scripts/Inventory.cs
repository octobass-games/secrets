using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Book Book;

    public Book GetBook()
    {
        return Book;
    }
    
    public void Pickup(Book book)
    {
        Book = book;
    }
    public bool Contains(string bookName)
    {
        return Book != null && Book.IsCalled(bookName);
    }

    public bool IsEmpty()
    {
        return Book == null;
    }

    public bool IsNotEmpty()
    {
        return !IsEmpty();
    }
}
