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
    public GameObject Stamp;
    public GameObject EndOfDayStamp;
    public Button NextPageButton;
    public Button PreviousPageButton;
    public GameObject CloseButton;
    public TMP_Text UniqueSalesTotal;
    public TMP_Text SalesRecordsTotal;
    public TMP_Text OutgoingCostsTotal;

    private int DailyTransactionsIndex;
    private List<GameObject> UniqueSales = new();
    private List<GameObject> SalesRecords = new();
    private List<GameObject> OutgoingCosts = new();
    private GameObject DailyTransactionsStamp;

    public void DisplayLog()
    {
        EndOfDayStamp.SetActive(false);
        CloseButton.SetActive(true);

        List<DailyTransactions> dailyTransactions = Bookkeeper.GetDailyTransactions();

        DailyTransactionsIndex = dailyTransactions.Count - 1;

        LogView.SetActive(true);

        ListDailyTransactions();
    }

    public void DisplayEndOfDayLog()
    {
        DisplayLog();

        CloseButton.SetActive(false);
        EndOfDayStamp.SetActive(true);
        Stamp.SetActive(false);
        PreviousPageButton.gameObject.SetActive(false);
        NextPageButton.gameObject.SetActive(false);
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
            Stamp.SetActive(true);
            NextPageButton.gameObject.SetActive(true);
        }
        else
        {
            Stamp.SetActive(false);
            NextPageButton.gameObject.SetActive(false);
        }

        DailyTransactions record = dailyTransactions[DailyTransactionsIndex];

        DateView.text = record.Date;

        foreach (BookSale bookSale in record.BookSales)
        {
            GameObject bookSaleView = Instantiate(SaleRecordPrefab);

            var texts = bookSaleView.GetComponentsInChildren<TMP_Text>();

            texts[0].text = bookSale.Name + " x" + bookSale.Quantity;
            texts[1].text = (bookSale.SellPrice * bookSale.Quantity).ToString();

            bookSaleView.transform.SetParent(SaleRecordParent.transform);
            bookSaleView.transform.localScale = Vector3.one;

            SalesRecords.Add(bookSaleView);
        }

        foreach (BookOrder bookOrder in record.BookOrders)
        {
            CreateOutgoingCost(bookOrder.Name + " x" + bookOrder.Quantity, bookOrder.TotalCost.ToString());
        }

        if (record.Tax != 0)
        {
            CreateOutgoingCost("Tax (12.5%)", record.Tax.ToString());
        }

        if (record.Rent != 0)
        {
            CreateOutgoingCost("Rent", record.Rent.ToString());
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

        Destroy(DailyTransactionsStamp);
    }

    private void CreateOutgoingCost(string name, string cost)
    {
        GameObject outgoing = Instantiate(BookOrderPrefab, OutgoingCostsParent.transform);

        var texts = outgoing.GetComponentsInChildren<TMP_Text>();

        texts[0].text = name;
        texts[1].text = cost;

        outgoing.transform.localScale = Vector3.one;

        OutgoingCosts.Add(outgoing);
    }
}
