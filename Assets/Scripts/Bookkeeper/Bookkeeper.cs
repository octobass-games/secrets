using System.Collections.Generic;
using UnityEngine;

public class Bookkeeper : MonoBehaviour, Savable
{
    public List<BookDefinition> Books;
    public TillView TillView;

    private DayDefinition Today;
    private List<BookSale> BookSalesToday;
    private int BankBalance = 10000;
    private List<SalesRecord> SalesRecords;
    private List<BookOrder> BookOrders;

    private System.Random RandomNumberGenerator = new System.Random();

    void Awake()
    {
        EventManager.Instance.Subscribe(GameEventType.BEGIN_DAY, OnBeginDay);
        EventManager.Instance.Subscribe(GameEventType.BANK_WITHDRAWAL, OnBankWithdrawal);
        EventManager.Instance.Subscribe(GameEventType.BOOK_ORDER, OnBookOrder);

        TillView.DisplayImmediately(BankBalance);
    }
    
    public void OnBeginDay(GameEvent @event)
    {
        SalesRecords = new();
        BookSalesToday = new();
        BookOrders = new();
        Today = @event.Day;
    }

    public void OnBankWithdrawal(GameEvent @event)
    {
        BankBalance -= @event.Amount;

        TillView.Display(BankBalance);
    }

    public void UpdateBooks()
    {
        UpdateSalesRecords();
        UpdateStock();
    }

    public bool IsAffordablePayment(int paymentAmount)
    {
        return BankBalance >= paymentAmount;
    }

    private void OnBookOrder(GameEvent @event)
    {
        foreach (var book in @event.BooksToOrder)
        {
            BookOrders.Add(new BookOrder(book.Name, @event.Amount));
        }
    }

    private void UpdateSalesRecords()
    {
        foreach (BookDefinition book in Books)
        {
            float likelihoodOfBookSale = CalculateLikelihoodOfSale(book);

            for (int i = 0; i < book.Stock; i++)
            {
                if (RandomNumberGenerator.Next(0, 100) < likelihoodOfBookSale)
                {
                    RegisterBookSale(book);
                }
            }
        }

        SalesRecords.Add(new SalesRecord(Today.Date, BookSalesToday));
    }

    public void IncrementBookPrice(BookDefinition book)
    {
        book.SellPrice++;
    }

    public void DecrementBookPrice(BookDefinition book)
    {
        book.SellPrice--;
    }

    private void UpdateStock()
    {
        foreach (BookDefinition Book in Books)
        {
            var bookOrder = BookOrders.Find(b => b.Name ==  Book.Name);

            if (bookOrder != null)
            {
                var costToOrder = bookOrder.Quantity * Book.CostToOrder;

                BankBalance -= costToOrder;
                Book.Stock += bookOrder.Quantity;
            }
        }
    }

    private void RegisterBookSale(BookDefinition book)
    {
        BankBalance += book.SellPrice;
        book.Stock--;

        BookSale sale = BookSalesToday.Find(b => b.Name == book.Name);

        if (sale == null)
        {
            sale = new BookSale(book.Name, book.SellPrice, 1);

            BookSalesToday.Add(sale);
        }
        else
        {
            sale.Quantity++;
        }
    }

    private float CalculateLikelihoodOfSale(BookDefinition book)
    {
        var sellPrice = book.SellPrice;
        var recommendedSellPrice = book.RecommendedSellPrice;

        var percentageDifference = ((book.SellPrice / book.RecommendedSellPrice) * 100) - 100;
        var halfPercentageDifference = percentageDifference / 2;

       return book.SellPrice > 2 * book.RecommendedSellPrice ? 0 : book.BaseLikelihoodToSell - halfPercentageDifference;
    }

    public void Save(SaveData saveData)
    {
        saveData.Bookkeeper = new BookkeeperData(BankBalance, SalesRecords);
    }

    public void Load(SaveData saveData)
    {
        BookkeeperData bookkeeperData = saveData.Bookkeeper;

        BankBalance = bookkeeperData.BankBalance; 
        SalesRecords = bookkeeperData.SalesRecords;
    }
}
