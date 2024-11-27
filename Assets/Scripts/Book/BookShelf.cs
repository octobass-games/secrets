using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bookshelf : MonoBehaviour
{
    public Transform BookContainer;
    public GameObject BookPrefab;
    public BookInspector BookInspector;
    public Bookkeeper Bookkeeper;

    public Transform ArtPosition;
    public Transform BiographyPosition;
    public Transform ChildrensPosition;
    public Transform CraftsPosition;
    public Transform EconomicsPosition;
    public Transform HealthPosition;
    public Transform HistoryPosition;
    public Transform HorrorPosition;
    public Transform OccultPosition;
    public Transform PoetryPosition;
    public Transform PotionsPosition;
    public Transform RecipesPosition;
    public Transform RomancePosition;
    public Transform SpellsPosition;
    public Transform ThrillerPosition;
    public Transform WeaponsPosition;
    public Transform WellbeingPosition;

    private List<GameObject> Books = new();

    public void PlaceBooks(List<BookDefinition> books)
    {
        ClearBooks();

        var booksByCategory = books.GroupBy(book => book.Category).ToList();

        foreach (var group in booksByCategory)
        {
            int i = 0;
            Transform startingPosition = GetParentTransform(group.Key);

            foreach (var book in group)
            {
                var b = Instantiate(BookPrefab, startingPosition);

                b.GetComponent<BookshelfBook>().Setup(book, Bookkeeper.MoveToTill, BookInspector.ShowBookInspector);
                b.transform.localPosition = b.transform.localPosition + new Vector3(i * 7, 0, 0);

                Books.Add(b);

                i++;
            }
        }

        //for (int i = 0; i < books.Count; i++)
        //{
        //    var book = books[i];
        //    var b = Instantiate(BookPrefab, BookContainer);

        //    b.GetComponent<BookshelfBook>().Setup(book, Bookkeeper.MoveToTill, BookInspector.ShowBookInspector);
        //    b.transform.position = b.transform.position + new Vector3(i * 7, 0, 0);

        //    Books.Add(b);
        //}
    }

    public void MoveToTill(BookDefinition book)
    {
        var bookshelfBook = Books.Find(b => b.GetComponent<BookshelfBook>().BookDefinition.IsEqual(book));

        bookshelfBook.GetComponent<Clickable>().enabled = false;
        bookshelfBook.GetComponent<EventOnHover>().enabled = false;
    }

    public void PutBookBack(BookDefinition book)
    {
        var bookshelfBook = Books.Find(b => b.GetComponent<BookshelfBook>().BookDefinition.IsEqual(book));

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
        for (int i = 0; i < Books.Count; i++)
        {
            Destroy(Books[i]);
        }

        Books.Clear();
    }

    private Transform GetParentTransform(BookCategory category)
    {
        switch (category)
        {
            case BookCategory.ART:
                return ArtPosition;
            case BookCategory.BIOGRAPHY:
                return BiographyPosition;
            case BookCategory.CHILDRENS:
                return ChildrensPosition;
            case BookCategory.CRAFTS:
                return CraftsPosition;
            case BookCategory.ECONOMICS:
                return EconomicsPosition;
            case BookCategory.HEALTH:
                return HealthPosition;
            case BookCategory.HISTORY:
                return HistoryPosition;
            case BookCategory.HORROR:
                return HorrorPosition;
            case BookCategory.OCCULT:
                return OccultPosition;
            case BookCategory.POETRY:
                return PoetryPosition;
            case BookCategory.POTIONS:
                return PotionsPosition;
            case BookCategory.RECIPES:
                return RecipesPosition;
            case BookCategory.ROMANCE:
                return RomancePosition;
            case BookCategory.SPELLS:
                return SpellsPosition;
            case BookCategory.THRILLER:
                return ThrillerPosition;
            case BookCategory.WEAPONS:
                return WeaponsPosition;
            case BookCategory.WELLBEING:
                return WellbeingPosition;
            default:
                return ArtPosition;
        }
    }
}