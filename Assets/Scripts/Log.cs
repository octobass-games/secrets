using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Log : MonoBehaviour
{
    public Bookkeeper Bookkeeper;
    public GameObject LogView;
    public TMP_Text DateView;
    public GameObject SaleRecordParent;
    public GameObject SaleRecordPrefab;
    public GameObject OutgoingCostsParent;
    public GameObject BookOrderPrefab;
    public Button NextPageButton;
    public Button PreviousPageButton;

    private int SalesRecordIndex;
    private List<GameObject> SalesRecords = new();
    private List<GameObject> OutgoingCosts = new();

    void Awake()
    {
        NextPageButton.onClick.AddListener(NextLog);
        PreviousPageButton.onClick.AddListener(PreviousLog);
    }

    public void DisplayLog()
    {
        SalesRecordIndex = 0;

        LogView.SetActive(true);

        ListSalesRecord();
    }

    public void NextLog()
    {
        SalesRecordIndex++;
        ListSalesRecord();
    }

    public void PreviousLog()
    {
        SalesRecordIndex--;
        ListSalesRecord();
    }

    public void HideLog()
    {
        LogView.SetActive(false);
    }

    private void ListSalesRecord()
    {
        ClearSalesRecords();
        ClearOutgoingCosts();

        List<SalesRecord> salesRecords = Bookkeeper.GetSalesRecords();
        List<BookOrder> bookOrders = new() {
            new("test", 1, 100)
        };

        salesRecords = new()
        {
            new("01/01", new()
            {
                new BookSale("Test", 100, 1)
            }),
            new("02/01", new()
            {
                new BookSale("Hello", 100, 1),
                new BookSale("Hello Again", 100, 1)
            })
        };

        if (SalesRecordIndex == 0)
        {
            PreviousPageButton.gameObject.SetActive(false);
        }
        else
        {
            PreviousPageButton.gameObject.SetActive(true);
        }

        if (SalesRecordIndex < salesRecords.Count - 1)
        {
            NextPageButton.gameObject.SetActive(true);
        }
        else
        {
            NextPageButton.gameObject.SetActive(false);
        }

        SalesRecord record = salesRecords[SalesRecordIndex];

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
    }

    private void ClearSalesRecords()
    {
        for (int i = 0; i < SalesRecords.Count; i++)
        {
            Destroy(SalesRecords[i]);
        }

        SalesRecords.Clear();
    }

    private void ClearOutgoingCosts()
    {
        for (int i = 0; i < OutgoingCosts.Count; i++)
        {
            Destroy(OutgoingCosts[i]);
        }

        OutgoingCosts.Clear();
    }
}
