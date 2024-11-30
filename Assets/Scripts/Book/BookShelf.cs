using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bookshelf : MonoBehaviour
{
    public Transform BookContainer;
    public GameObject BookPrefab;
    public GameObject HollowBookPrefab;
    public BookInspector BookInspector;
    public Bookkeeper Bookkeeper;
    public History History;

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
    public Transform HollowBooksPosition;

    private List<GameObject> Books = new();
    private List<GameObject> HollowBooks = new();

    public void PlaceBooks(List<BookDefinition> books, List<BookDefinition> hollowBooks, GameObject tillBook)
    {
        BookDefinition tillBookDefinition = null;

        if (tillBook != null)
        {
            tillBookDefinition = tillBook.GetComponent<Book>().BookDefinition;
        }

        ClearBooks(tillBookDefinition);

        var booksByCategory = books.GroupBy(book => book.Category).ToList();

        foreach (var group in booksByCategory)
        {
            int i = 0;
            Transform startingPosition = GetParentTransform(group.Key);

            foreach (var book in group)
            {
                if (book != tillBookDefinition)
                {
                    var b = Instantiate(BookPrefab, startingPosition);

                    b.GetComponent<BookshelfBook>().Setup(book, Bookkeeper.MoveToTill, BookInspector.ShowBookInspector);
                    b.transform.localPosition = b.transform.localPosition + new Vector3(i * 7, 0, 0);

                    Books.Add(b);
                }

                i++;
            }
        }

        for (int i = 0; i < hollowBooks.Count; i++)
        {
            int index = i;

            var hollowBook = hollowBooks[index];

            if (hollowBook != tillBookDefinition)
            {
                var hollowBookGo = Instantiate(HollowBookPrefab, HollowBooksPosition);

                hollowBookGo.transform.position = new Vector3(hollowBookGo.transform.position.x + i * 7, hollowBookGo.transform.position.y, hollowBookGo.transform.position.z);
                hollowBookGo.GetComponent<BookshelfBook>().Setup(hollowBook, OnPickup, OnInspect);
                
                HollowBooks.Add(hollowBookGo);
            }
        }
    }

    public void MoveToTill(BookDefinition book)
    {
        var bookshelfBook = FindBook(book);

        bookshelfBook.GetComponent<Clickable>().enabled = false;
        bookshelfBook.GetComponent<EventOnHover>().enabled = false;
    }

    public void PutBookBack(BookDefinition book)
    {
        var bookshelfBook = FindBook(book);

        var bookshelfBookAnimator = bookshelfBook.GetComponentInChildren<Animator>();

        if (bookshelfBookAnimator != null)
        {
            bookshelfBookAnimator.SetTrigger("place");
        }

        bookshelfBook.GetComponent<Clickable>().enabled = true;
        bookshelfBook.GetComponent<EventOnHover>().enabled = true;
    }

    private bool IsFirstBookAndInspectionNotDone(BookDefinition hollowBook)
    {
        return hollowBook.Item != null && hollowBook.Item.Name == "Rat poison" && !History.Contains("hollow.book.discovered");
    }

    private void OnPickup(BookDefinition hollowBook)
    {
        if (IsFirstBookAndInspectionNotDone(hollowBook))
        {
            BookInspector.ShowBookInspector(hollowBook);
            Bookkeeper.MoveToTill(hollowBook);
        }
        else
        {
            Bookkeeper.MoveToTill(hollowBook);
        }
    }

    private void OnInspect(BookDefinition hollowBook)
    {
        if (IsFirstBookAndInspectionNotDone(hollowBook))
        {
            BookInspector.ShowBookInspector(hollowBook);
            Bookkeeper.MoveToTill(hollowBook);
        }
        else
        {
            BookInspector.ShowBookInspector(hollowBook);
        }
    }

    private GameObject FindBook(BookDefinition book)
    {
        if (book.IsHollow)
        {
            return HollowBooks.Find(b => {
                var bookDefinition = b.GetComponent<BookshelfBook>().BookDefinition;

                return bookDefinition.Name == book.Name && (bookDefinition.Item == null && book.Item == null || bookDefinition.Item.Name == book.Item.Name);
            });
        }
        else
        {
            return Books.Find(b => b.GetComponent<BookshelfBook>().BookDefinition.IsEqual(book));
        }
    }

    private void ClearBooks(BookDefinition tillBook)
    {
        var tillBookIndex = -1;

        for (int i = 0; i < Books.Count; i++)
        {
            var book = Books[i].GetComponent<BookshelfBook>().BookDefinition;

            if (book != tillBook)
            {
                Destroy(Books[i]);
            }
            else
            {
                tillBookIndex = i;
            }
        }

        GameObject go = null;

        if (tillBookIndex != -1)
        {
            go = Books[tillBookIndex];
        }

        Books.Clear();

        if (go != null) {
            Books.Add(go);
        }

        tillBookIndex = -1;
        go = null;

        for (int i = 0; i < HollowBooks.Count; i++)
        {
            var book = HollowBooks[i].GetComponent<BookshelfBook>().BookDefinition;

            if (book != tillBook)
            {
                Destroy(HollowBooks[i]);
            }
            else
            {
                tillBookIndex = i;
            }
        }

        if (tillBookIndex != -1)
        {
            go = HollowBooks[tillBookIndex];
        }

        HollowBooks.Clear();
        
        if (go != null)
        {
            HollowBooks.Add(go);
        }
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