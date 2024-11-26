using TMPro;
using UnityEngine;

public class BookInspector : MonoBehaviour
{
    public GameObject InspectorView;
    public Bookkeeper Bookkeeper;
    public TMP_Text BookTitleView;
    public GameObject BookHollow;
    public GameObject Knife;
    public History History;

    private BookDefinition Book;

    public void ShowInspector(BookDefinition definition)
    {
        Book = definition;

        InspectorView.SetActive(true);
        BookTitleView.text = definition.Name;

        if  (definition.IsHollow)
        {
            BookHollow.SetActive(true);
        }

        if (History.Contains("knife.unlocked"))
        {
            Knife.SetActive(true);
        }
    }

    public void HideInspector()
    {
        InspectorView.SetActive(false);
        BookHollow.SetActive(false);
        Knife.SetActive(false);
        Book = null;
    }

    public void HollowBook()
    {
        if (!Book.IsHollow)
        {
            Book = Bookkeeper.HollowBook(Book);
            BookHollow.SetActive(true);
            Knife.SetActive(false);
        }
    }

    public void DisplayItems()
    {
        Debug.Log("Items to display");
    }

    public void InsertItem(ItemDefinition item)
    {
        Bookkeeper.InsertIntoHollowBook(Book, item);
    }
}
