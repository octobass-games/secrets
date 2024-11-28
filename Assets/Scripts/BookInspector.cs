using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BookInspector : MonoBehaviour
{
    public GameObject InspectorView;
    public Bookkeeper Bookkeeper;
    public TMP_Text BookTitleView;
    public GameObject BookHollow;
    public GameObject Knife;
    public History History;
    public Image BookCover;
    public Image Item;

    public GameObject ItemPrefab;
    public GameObject ItemsView;
    public Transform ItemContainer;

    private BookDefinition Book;
    private List<GameObject> Items = new();

    public void ShowBookInspector(BookDefinition definition)
    {
        Book = definition;

        InspectorView.SetActive(true);
        BookTitleView.text = definition.Name;
        BookCover.color = definition.Colour;

        if  (definition.IsHollow)
        {
            BookHollow.SetActive(true);

            if (definition.Item != null)
            {
                Item.gameObject.SetActive(true);
                Item.sprite = definition.Item.Sprite;
            }
            else
            {
                Item.gameObject.SetActive(false);
            }
        }
        else
        {
            BookHollow.SetActive(false);
        }

        if (History.Contains("knife.unlocked"))
        {
            Knife.SetActive(true);
        }
    }

    public void HideBookInspector()
    {
        InspectorView.SetActive(false);
        BookHollow.SetActive(false);
        Knife.SetActive(false);
        Book = null;
    }

    public void ShowItemSelector()
    {
        ClearItems();

        ItemsView.SetActive(true);

        var itemsInStock = Bookkeeper.GetAvailableItems();

        foreach (var item in itemsInStock)
        {
            GameObject itemView = Instantiate(ItemPrefab, ItemContainer);

            itemView.GetComponent<Item>().Setup(item, InsertItem);

            Items.Add(itemView);
        }
    }

    public void HideItemSelector()
    {
        ItemsView.SetActive(false);
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

    public void InsertItem(ItemDefinition item)
    {
        Bookkeeper.InsertIntoHollowBook(Book, item);
        HideItemSelector();
    }

    private void ClearItems()
    {
        for (int i = 0; i < Items.Count; i++)
        {
            Destroy(Items[i]);
        }

        Items.Clear();
    }
}
