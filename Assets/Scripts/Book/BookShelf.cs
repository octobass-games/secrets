using System.Collections.Generic;
using UnityEngine;

public class Bookshelf : MonoBehaviour
{
    public List<BookshelfBook> Books;
    public Transform BookContainer;
    public GameObject BookPrefab;
    public BookInspector BookInspector;
    public Bookkeeper Bookkeeper;

    private List<GameObject> BooksA = new();

    public void PlaceBooks(List<BookDefinition> books)
    {
        ClearBooks();

        for (int i = 0; i < books.Count; i++)
        {
            var book = books[i];
            var b = Instantiate(BookPrefab, BookContainer);

            b.GetComponent<BookshelfBook>().Setup(book, Bookkeeper.MoveToTill, BookInspector.ShowBookInspector);
            b.transform.position = b.transform.position + new Vector3(b.transform.position.x + i * 7, 0, 0);

            BooksA.Add(b);
        }
        //foreach (var book in Books)
        //{
        //   var bookDefinition = books.Find(a => a.IsEqual(book.BookDefinition));

        //    if (bookDefinition != null)
        //    {
        //        book.gameObject.SetActive(true);
        //    }
        //    else
        //    {
        //        book.gameObject.SetActive(false);
        //    }
        //}
    }

    public void MoveToTill(BookDefinition book)
    {
        var bookshelfBook = BooksA.Find(b => b.GetComponent<BookshelfBook>().BookDefinition.IsEqual(book));

        bookshelfBook.GetComponent<Clickable>().enabled = false;
        bookshelfBook.GetComponent<EventOnHover>().enabled = false;
    }

    public void PutBookBack(BookDefinition book)
    {
        var bookshelfBook = BooksA.Find(b => b.GetComponent<BookshelfBook>().BookDefinition.IsEqual(book));

        var bookshelfBookAnimator = bookshelfBook.GetComponentInChildren<Animator>();

        if (bookshelfBookAnimator != null)
        {
            bookshelfBookAnimator.SetTrigger("place");
        }

        bookshelfBook.GetComponent<Clickable>().enabled = true;
        bookshelfBook.GetComponent<EventOnHover>().enabled = true;
    }

    private void ClearBooks()
    {
        for (int i = 0; i < BooksA.Count; i++)
        {
            Destroy(BooksA[i]);
        }

        BooksA.Clear();
    }
}