using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BookInspector : MonoBehaviour
{
    public GameObject InspectorView;
    public Bookkeeper Bookkeeper;
    public TMP_Text BookTitleView;
    public TMP_Text RRPView;
    public TMP_Text StockView;
    public TMP_Text PriceView;
    public TMP_Text BookDesciptionView;
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
        BookDesciptionView.text = definition.Description;
        BookCover.color = definition.Colour;
        RRPView.text = "RRP: " +definition.RecommendedSellPrice + " coins";
        PriceView.text = "Price: " + definition.SellPrice + " coins";
        StockView.text = "Stock: " + definition.Stock ;

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
            Item.gameObject.SetActive(false);
        }
    }

    public void InsertItem(ItemDefinition item)
    {
        Bookkeeper.InsertIntoHollowBook(Book, item);
        
        Item.gameObject.SetActive(true);
        Item.sprite = item.Sprite;
        
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
