using System.Collections.Generic;
using UnityEngine;

class Bookshelf : MonoBehaviour
{
    public Bookkeeper Bookkeeper;
    public List<BookshelfBook> Books;

    public GameObject TillBook;
    public Transform TillBookPosition;
    public GameObject TillBookPrefab;

    void Start()
    {
        foreach (var book in Books)
        {
            if (Bookkeeper.InStock(book.BookDefinition))
            {
                book.gameObject.SetActive(true);
            }
            else
            {
                // TODO: flip this back to false
                book.gameObject.SetActive(true);
            }
        }
    }

    public void MoveToTill(BookDefinition book)
    {
        if (TillBook != null)
        {
            Destroy(TillBook);
        }

        var bookshelfBook = Books.Find(b => b.BookDefinition.IsEqual(book));

        TillBook = Instantiate(TillBookPrefab);
        TillBook.transform.position = TillBookPosition.position;
        TillBook.GetComponent<Book>().BookDefinition = book;
        TillBook.GetComponent<Book>().Setup();
        TillBook.gameObject.SetActive(true);

        bookshelfBook.GetComponent<Clickable>().enabled = false;
        bookshelfBook.GetComponent<EventOnHover>().enabled = false;
    }
}