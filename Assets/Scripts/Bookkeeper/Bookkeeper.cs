using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Bookkeeper : MonoBehaviour, Savable
{
    public List<BookDefinition> Books;
    public TillView TillView;
    public Bookshelf Bookshelf;
    public GameObject TillBook;
    public Transform TillBookPosition;
    public GameObject TillBookPrefab;

    private int BankBalance = 10000;

    private DayDefinition Today;
    private DailyTransactions TransactionsToday;
    private List<DailyTransactions> DailyTransactions = new();
    
    private System.Random RandomNumberGenerator = new System.Random();

    void Awake()
    {
        Books = Books.Select(b => Instantiate(b)).ToList();
    }

    void OnEnable()
    {
        EventManager.Instance.Subscribe(GameEventType.BEGIN_DAY, OnBeginDay);
        EventManager.Instance.Subscribe(GameEventType.END_DAY, OnEndDay);
        EventManager.Instance.Subscribe(GameEventType.BANK_WITHDRAWAL, OnBankWithdrawal);
        EventManager.Instance.Subscribe(GameEventType.BOOK_ORDER, OnBookOrder);
        EventManager.Instance.Subscribe(GameEventType.INVENTORY_SELL, OnBookSell);
    }

    void OnDisable()
    {
        EventManager.Instance.Unsubscribe(GameEventType.BEGIN_DAY, OnBeginDay);
        EventManager.Instance.Unsubscribe(GameEventType.END_DAY, OnEndDay);
        EventManager.Instance.Unsubscribe(GameEventType.BANK_WITHDRAWAL, OnBankWithdrawal);
        EventManager.Instance.Unsubscribe(GameEventType.BOOK_ORDER, OnBookOrder);
        EventManager.Instance.Unsubscribe(GameEventType.INVENTORY_SELL, OnBookSell);
    }

    public List<DailyTransactions> GetDailyTransactions()
    {
        return DailyTransactions;
    }
    
    public void OnBeginDay(GameEvent @event)
    {
        Today = @event.Day;

        TransactionsToday = new DailyTransactions(Today.Date, new(), new(), new(), 0, 0);
        DailyTransactions.Add(TransactionsToday);

        var booksInStock = Books.FindAll(InStock).ToList();

        Bookshelf.PlaceBooks(booksInStock);
        TillView.DisplayImmediately(BankBalance);
    }

    public void OnEndDay(GameEvent @event)
    {
        UpdateBooks();
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
            var bookDefinition = Books.Find(b => b.IsEqual(book));

            TransactionsToday.BookOrders.Add(new BookOrder(book.Name, @event.Amount, @event.Amount * bookDefinition.CostToOrder));
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

        DailyTransactions.Add(TransactionsToday);
    }

    public void IncrementBookPrice(BookDefinition book)
    {
        book.SellPrice++;
    }

    public void DecrementBookPrice(BookDefinition book)
    {
        book.SellPrice--;
    }

    public bool InStock(BookDefinition book)
    {
        return Books.Find(b => b.IsEqual(book)).Stock > 0;
    }

    private void UpdateStock()
    {
        foreach (BookDefinition Book in Books)
        {
            var bookOrder = TransactionsToday.BookOrders.Find(b => b.Name ==  Book.Name);

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

        BookSale sale = TransactionsToday.BookSales.Find(b => b.Name == book.Name);

        if (sale == null)
        {
            sale = new BookSale(book.Name, book.SellPrice, 1);

            TransactionsToday.BookSales.Add(sale);
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
        var booksData = Books.Select(b => new BookData(b.Name, b.SellPrice, b.Stock)).ToList();

        saveData.Bookkeeper = new BookkeeperData(BankBalance, booksData, DailyTransactions);
    }

    public void Load(SaveData saveData)
    {
        BookkeeperData bookkeeperData = saveData.Bookkeeper;
        List<BookData> booksData = bookkeeperData.Books;

        BankBalance = bookkeeperData.BankBalance; 
        DailyTransactions = bookkeeperData.DailyTransactions;
        Books.ForEach(book =>
        {
            var bookData = booksData.Find(b => book.Name == b.Name);

            if (bookData != null)
            {
                book.SellPrice = bookData.SellPrice;
            }
        });

        TillView.DisplayImmediately(BankBalance);
    }

    public void MoveToTill(BookDefinition book)
    {
        Bookshelf.MoveToTill(book);   

        if (TillBook != null)
        {
            var bookDefinition = TillBook.GetComponent<Book>().BookDefinition;

            Bookshelf.PutBookBack(bookDefinition);

            //var tillBookBooshelfBook = Books.Find(b => b.BookDefinition.IsEqual(bookDefinition));
            //tillBookBooshelfBook.gameObject.SetActive(true);

            Destroy(TillBook);
        }

        TillBook = Instantiate(TillBookPrefab);
        TillBook.transform.position = TillBookPosition.position;
        TillBook.GetComponent<Book>().BookDefinition = book;
        TillBook.GetComponent<Book>().Setup();
        TillBook.gameObject.SetActive(true);
    }

    public bool NoBookAtTill()
    {
        return TillBook == null;
    }

    public bool IsBookAtTill(BookDefinition book)
    {
        return TillBook != null && TillBook.GetComponent<Book>().BookDefinition.IsEqual(book);
    }

    private void OnBookSell(GameEvent _)
    {
        if (TillBook != null)
        {
            var book = TillBook.GetComponent<Book>().BookDefinition;

            var b = Books.Find(b => b.IsEqual(book));

            RegisterBookSale(b);
            TillView.Display(BankBalance);
        }
    }
}
