using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Log : MonoBehaviour
{
    public Bookkeeper Bookkeeper;
    public GameObject LogView;
    public TMP_Text DateView;
    public GameObject UniqueSaleParent;
    public GameObject UniqueSalePrefab;
    public GameObject SaleRecordParent;
    public GameObject SaleRecordPrefab;
    public GameObject OutgoingCostsParent;
    public GameObject BookOrderPrefab;
    public Button NextPageButton;
    public Button PreviousPageButton;

    private int DailyTransactionsIndex;
    private List<GameObject> UniqueSales = new();
    private List<GameObject> SalesRecords = new();
    private List<GameObject> OutgoingCosts = new();

    void Awake()
    {
        NextPageButton.onClick.AddListener(NextLog);
        PreviousPageButton.onClick.AddListener(PreviousLog);
    }

    public void DisplayLog()
    {
        DailyTransactionsIndex = 0;

        LogView.SetActive(true);

        ListDailyTransactions();
    }

    public void NextLog()
    {
        DailyTransactionsIndex++;
        ListDailyTransactions();
    }

    public void PreviousLog()
    {
        DailyTransactionsIndex--;
        ListDailyTransactions();
    }

    public void HideLog()
    {
        LogView.SetActive(false);
    }

    private void ListDailyTransactions()
    {
        ClearDailyTransactions();

        List<DailyTransactions> dailyTransactions = Bookkeeper.GetDailyTransactions();
        List<BookOrder> bookOrders = new() {
            new("test", 1, 100)
        };

        dailyTransactions = new()
        {
            new("01/01", new() {
                new("Hello", "World", 100)
            }, new()
            {
                new("Test", 100, 100)
            }, new()
            {
                new("Test", 100, 1000)
            }),
            new("01/01", new() {
                new("Hello", "World Things", 200)
            }, new()
            {
                new("Test", 100, 100)
            }, new()
            {
                new("Test", 100, 1000)
            }),
            new("01/01", new() {
                new("Hello", "World Things again", 300)
            }, new()
            {
                new("Test", 100, 100),
                new("Test", 100, 100)
            }, new()
            {
                new("Test", 100, 1000)
            })
        };

        if (DailyTransactionsIndex == 0)
        {
            PreviousPageButton.gameObject.SetActive(false);
        }
        else
        {
            PreviousPageButton.gameObject.SetActive(true);
        }

        if (DailyTransactionsIndex < dailyTransactions.Count - 1)
        {
            NextPageButton.gameObject.SetActive(true);
        }
        else
        {
            NextPageButton.gameObject.SetActive(false);
        }

        DailyTransactions record = dailyTransactions[DailyTransactionsIndex];

        DateView.text = record.Date;

        foreach (BookSale bookSale in record.BookSales)
        {
            GameObject bookSaleView = Instantiate(SaleRecordPrefab);

            var texts = bookSaleView.GetComponentsInChildren<TMP_Text>();

            texts[0].text = bookSale.Name;
            texts[1].text = bookSale.SellPrice.ToString();

            bookSaleView.transform.SetParent(SaleRecordParent.transform);
            bookSaleView.transform.localScale = Vector3.one;

            SalesRecords.Add(bookSaleView);
        }

        foreach (BookOrder bookOrder in bookOrders)
        {
            GameObject outgoing = Instantiate(BookOrderPrefab);

            var texts = outgoing.GetComponentsInChildren<TMP_Text>();

            texts[0].text = bookOrder.Name + " x" + bookOrder.Quantity;
            texts[1].text = bookOrder.TotalCost.ToString();

            outgoing.transform.SetParent(OutgoingCostsParent.transform);
            outgoing.transform.localScale = Vector3.one;

            OutgoingCosts.Add(outgoing);
        }

        foreach (UniqueBookSale uniqueBookSale in record.UniqueBookSales)
        {
            GameObject uniqueSale = Instantiate(UniqueSalePrefab);

            var texts = uniqueSale.GetComponentsInChildren<TMP_Text>();

            texts[0].text = "sold to " + uniqueBookSale.BuyerName;
            texts[1].text = uniqueBookSale.BookName;
            texts[2].text = uniqueBookSale.SellPrice.ToString();

            uniqueSale.transform.SetParent(UniqueSaleParent.transform);
            uniqueSale.transform.localScale = Vector3.one;

            UniqueSales.Add(uniqueSale);
        }
    }

    private void ClearDailyTransactions()
    {
        for (int i = 0; i < SalesRecords.Count; i++)
        {
            Destroy(SalesRecords[i]);
        }

        SalesRecords.Clear();

        for (int i = 0; i < OutgoingCosts.Count; i++)
        {
            Destroy(OutgoingCosts[i]);
        }

        OutgoingCosts.Clear();

        for (int i = 0; i < UniqueSales.Count; i++)
        {
            Destroy(UniqueSales[i]);
        }

        UniqueSales.Clear();
    }
}
