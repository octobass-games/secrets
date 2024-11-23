
using System.Collections.Generic;
using UnityEngine;

class Bookshelf : MonoBehaviour
{
    public Bookkeeper Bookkeeper;
    public List<Book> Books;

    public GameObject TillBook;
    public Transform TillBookPosition;
    public GameObject RomanceBookPrefab;

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

        GameObject prefab = GetPrefabToInstantiate(bookshelfBook);

        TillBook = Instantiate(prefab);
        TillBook.transform.position = TillBookPosition.position;
        TillBook.GetComponent<Book>().BookDefinition = book;
        TillBook.gameObject.SetActive(true);

        bookshelfBook.gameObject.SetActive(false);
    }

    private GameObject GetPrefabToInstantiate(Book book)
    {
        switch (book.BookDefinition.Category)
        {
            case BookCategory.ROMANCE:
                return RomanceBookPrefab;
            default:
                return null;
        }
    }
}