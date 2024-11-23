using System.Collections.Generic;
using UnityEngine;

public class Bookshelf : MonoBehaviour
{
    public List<BookshelfBook> Books;

    public void PlaceBooks(List<BookDefinition> books)
    {
        foreach (var book in Books)
        {
            var bookDefinition = books.Find(a => a.IsEqual(book.BookDefinition));

            if (bookDefinition != null)
            {
                book.gameObject.SetActive(true);
            }
            else
            {
                book.gameObject.SetActive(false);
            }
        }
    }

    public void MoveToTill(BookDefinition book)
    {
        var bookshelfBook = Books.Find(b => b.BookDefinition.IsEqual(book));

        bookshelfBook.GetComponent<Clickable>().enabled = false;
        bookshelfBook.GetComponent<EventOnHover>().enabled = false;
    }
}