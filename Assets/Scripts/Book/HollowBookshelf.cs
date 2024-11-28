using System.Collections.Generic;
using UnityEngine;

public class HollowBookshelf : MonoBehaviour
{
    public Bookkeeper Bookkeeper;
    public GameObject HollowBookPrefab;
    public Transform StartingPosition;
    public BookInspector BookInspector;

    private List<GameObject> HollowBooks = new();

    public void PlaceBooks(List<BookDefinition> books)
    {
        ClearHollowBooks();

        for (int i = 0; i < books.Count; i++)
        {
            int index = i;

            var hollowBook = Instantiate(HollowBookPrefab, StartingPosition);

            hollowBook.transform.position = new Vector3(hollowBook.transform.position.x + i * 7, hollowBook.transform.position.y, hollowBook.transform.position.z);
            hollowBook.GetComponent<BookshelfBook>().Setup(books[index], Bookkeeper.MoveToTill, BookInspector.ShowBookInspector);

            HollowBooks.Add(hollowBook);
        }
    }

    public void MoveToTill(BookDefinition book)
    {
        var hollowBook = HollowBooks.Find(b => {
            var bookDefinition = b.GetComponent<BookshelfBook>().BookDefinition;

            return bookDefinition.Name == book.Name && (bookDefinition.Item == null && book.Item == null || bookDefinition.Item.Name == book.Item.Name);
            });

        hollowBook.GetComponent<Clickable>().enabled = false;
        hollowBook.GetComponent<EventOnHover>().enabled = false;
    }

    public void PutBookBack(BookDefinition book)
    {
        var hollowBook = HollowBooks.Find(b => {
            var bookDefinition = b.GetComponent<BookshelfBook>().BookDefinition;

            return bookDefinition.Name == book.Name && (bookDefinition.Item == null && book.Item == null || bookDefinition.Item.Name == book.Item.Name);
        });

        var hollowBookAnimator = hollowBook.GetComponentInChildren<Animator>();

        if (hollowBookAnimator != null)
        {
            hollowBookAnimator.SetTrigger("place");
        }

        hollowBook.GetComponent<Clickable>().enabled = true;
        hollowBook.GetComponent<EventOnHover>().enabled = true;
    }

    private void ClearHollowBooks()
    {
        for (int i = 0;i < HollowBooks.Count;i++)
        {
            Destroy(HollowBooks[i]);
        }

        HollowBooks.Clear();
    }
}
