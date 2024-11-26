using System.Collections.Generic;
using UnityEngine;

public class HollowBookshelf : MonoBehaviour
{
    public Bookkeeper Bookkeeper;
    public GameObject HollowBookPrefab;
    public Transform StartingPosition;

    private List<GameObject> HollowBooks = new();

    public void PlaceBooks(List<BookDefinition> books)
    {
        ClearHollowBooks();

        for (int i = 0; i < books.Count; i++)
        {
            int index = i;

            var hollowBook = Instantiate(HollowBookPrefab, StartingPosition);

            hollowBook.transform.position = new Vector3(hollowBook.transform.position.x + i * 7, hollowBook.transform.position.y, hollowBook.transform.position.z);
            hollowBook.GetComponent<HollowBookshelfBook>().Setup(books[index], Bookkeeper);

            HollowBooks.Add(hollowBook);
        }
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
