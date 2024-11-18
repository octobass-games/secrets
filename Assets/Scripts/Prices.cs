using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Prices : MonoBehaviour
{
    public Bookkeeper Bookkeeper;
    public GameObject PriceView;
    public GameObject PriceEntryParent;
    public GameObject PriceEntryPrefab;
    public TMP_InputField SearchField;

    private List<GameObject> PriceEntries = new();
    private BookCategory SelectedCategory;

    void Awake()
    {
        SearchField.onValueChanged.AddListener((string title) => ListBookPrices(SelectedCategory, title));
    }

    public void DisplayPrices()
    {
        PriceView.SetActive(true);
        ListBookPrices(BookCategory.ALL, null);
    }

    public void HidePrices()
    {
        PriceView.SetActive(false);
    }

    public void ListAllBookPrices() => ListBookPrices(BookCategory.ALL, null);
    public void ListRomanceBookPrices() => ListBookPrices(BookCategory.ROMANCE, null);

    private void ListBookPrices(BookCategory category, string title)
    {
        ClearPriceEntries();

        SelectedCategory = category;

        var books = Bookkeeper.Books;

        if (category != BookCategory.ALL)
        {
            books = books.FindAll(b => b.Category == category).ToList();
        }

        if (title != null && title != "")
        {
            books = books.FindAll(b => b.Name.ToLower().Contains(title.ToLower()));
        }

        foreach (var book in books)
        {
            var priceEntry = Instantiate(PriceEntryPrefab);

            var texts = priceEntry.GetComponentsInChildren<TMP_Text>();

            texts[0].text = book.Name;
            texts[2].text = book.SellPrice.ToString();

            var buttons = priceEntry.GetComponentsInChildren<Button>();

            buttons[0].onClick.AddListener(() => {
                Bookkeeper.DecrementBookPrice(book);
                texts[2].text = book.SellPrice.ToString();
            });
            buttons[1].onClick.AddListener(() => {
                Bookkeeper.IncrementBookPrice(book);
                texts[2].text = book.SellPrice.ToString();
            });

            priceEntry.transform.SetParent(PriceEntryParent.transform);
            priceEntry.transform.localScale = Vector3.one;

            PriceEntries.Add(priceEntry);
        }
    }

    private void ClearPriceEntries()
    {
        for (int i = 0; i < PriceEntries.Count; i++)
        {
            Destroy(PriceEntries[i]);
        }

        PriceEntries.Clear();
    }
}
