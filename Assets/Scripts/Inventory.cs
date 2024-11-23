using UnityEngine;

public class Inventory : MonoBehaviour
{
    private BookshelfBook Book;

    public BookshelfBook GetBook()
    {
        return Book;
    }
    
    public void Pickup(BookshelfBook book)
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
