using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bookkeeper : MonoBehaviour, Savable
{
    public List<BookDefinition> Books;
    public List<ItemDefinition> Items;
    public TillView TillView;
    public Bookshelf Bookshelf;
    public HollowBookshelf HollowBookshelf;
    public GameObject TillBook;
    public Transform TillBookPosition;
    public GameObject TillBookPrefab;
    public DialogueManager DialogueManager;
    public Line TooManyHollowBooks;
    public List<BookDefinition> HollowBooks = new();

    private int BankBalance = 10000;

    private DayDefinition Today;
    private DailyTransactions TransactionsToday;
    private List<DailyTransactions> DailyTransactions = new();
    
    private System.Random RandomNumberGenerator = new System.Random();

    void Awake()
    {
        Books = Books.Select(b => Instantiate(b)).ToList();
        HollowBooks = HollowBooks.Select(b => Instantiate(b)).ToList();
    }

    void OnEnable()
    {
        EventManager.Instance.Subscribe(GameEventType.BEGIN_DAY, OnBeginDay);
        EventManager.Instance.Subscribe(GameEventType.CLOSE_SHOP, OnCloseShop);
        EventManager.Instance.Subscribe(GameEventType.RENT_PAYMENT, OnRentPayment);
        EventManager.Instance.Subscribe(GameEventType.BOOK_ORDER, OnBookOrder);
        EventManager.Instance.Subscribe(GameEventType.INVENTORY_SELL, OnBookSell);
    }

    void OnDisable()
    {
        EventManager.Instance.Unsubscribe(GameEventType.BEGIN_DAY, OnBeginDay);
        EventManager.Instance.Unsubscribe(GameEventType.CLOSE_SHOP, OnCloseShop);
        EventManager.Instance.Unsubscribe(GameEventType.RENT_PAYMENT, OnRentPayment);
        EventManager.Instance.Unsubscribe(GameEventType.BOOK_ORDER, OnBookOrder);
        EventManager.Instance.Unsubscribe(GameEventType.INVENTORY_SELL, OnBookSell);
    }

    public List<DailyTransactions> GetDailyTransactions()
    {
        return DailyTransactions;
    }

    public DailyTransactions GetEndOfDayTransactions()
    {
        return TransactionsToday;
    }

    private void OnRentPayment(GameEvent @event)
    {
        int rent = @event.Amount;

        TransactionsToday.Rent = rent;

        Withdraw(rent);
    }
    
    public void OnBeginDay(GameEvent @event)
    {
        Today = @event.Day;

        TransactionsToday = new DailyTransactions(Today.Date, new(), new(), new(), 0, 0);
        DailyTransactions.Add(TransactionsToday);

        var booksInStock = Books.FindAll(InStock).ToList();

        Bookshelf.PlaceBooks(booksInStock);
        HollowBookshelf.PlaceBooks(HollowBooks);
        TillView.DisplayImmediately(BankBalance);
    }

    public List<ItemDefinition> GetAvailableItems()
    {
        return Items.FindAll(i => i.Stock > 0).ToList();
    }

    public void OnCloseShop(GameEvent @event)
    {
        UpdateBooks();
    }

    private void Withdraw(int amount)
    {
        BankBalance -= amount;

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
                    RegisterBookSale(book, null);
                }
            }
        }
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

    public BookDefinition HollowBook(BookDefinition book)
    {
        if (HollowBooks.Count >= 3)
        {
            DialogueManager.Begin(TooManyHollowBooks, null);

            return null;
        }
        else
        {
            var b = Books.Find(b => b.IsEqual(book));
            b.Stock--;

            var hollow = Instantiate(book);

            HollowBooks.Add(hollow);

            var booksInStock = Books.FindAll(InStock).ToList();
            hollow.IsHollow = true;
            Bookshelf.PlaceBooks(booksInStock);

            HollowBookshelf.PlaceBooks(HollowBooks);

            return hollow;
        }
    }

    public void InsertIntoHollowBook(BookDefinition hollowBook, ItemDefinition item)
    {
        hollowBook.Item = item;
        HollowBookshelf.PlaceBooks(HollowBooks);
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

    private void RegisterBookSale(BookDefinition book, CharacterDefinition character)
    {
        BankBalance += book.SellPrice;
        book.Stock--;

        if (character == null)
        {
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
        else
        {
            TransactionsToday.UniqueBookSales.Add(new UniqueBookSale(character.Name, book.Name, book.SellPrice));
        }
    }

    private void RegisterHollowBookSale(BookDefinition book, CharacterDefinition character)
    {
        BankBalance += book.Item.SellPrice;

        TransactionsToday.UniqueBookSales.Add(new UniqueBookSale(character.Name, book.Name, book.Item.SellPrice));

        var b = HollowBooks.Find(b => b.Item.Name == book.Item.Name);

        HollowBooks.Remove(b);
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
        var booksData = Books.Select(b => new BookData(b.Name, b.SellPrice, b.Stock, null)).ToList();
        var hollowBooksData = HollowBooks.Select(b => new BookData(b.Name, b.SellPrice, b.Stock, b.Item != null ? b.Item.Name : null)).ToList();

        saveData.Bookkeeper = new BookkeeperData(BankBalance, booksData, hollowBooksData, DailyTransactions);
    }

    public void Load(SaveData saveData)
    {
        BookkeeperData bookkeeperData = saveData.Bookkeeper;
        List<BookData> booksData = bookkeeperData.Books;
        List<BookData> hollowBooksData = bookkeeperData.HollowBooks;

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
        HollowBooks = hollowBooksData.Select(hollowBookData =>
        {
            var bookDefinition = Books.Find(book => book.Name == hollowBookData.Name);

            var hollowBook = Instantiate(bookDefinition);

            if (hollowBookData.Item != null)
            {
                var item = Items.Find(item => item.Name == hollowBookData.Item);

                hollowBook.Item = item;
            }

            return hollowBook;
        }).ToList();

        TillView.DisplayImmediately(BankBalance);
    }

    public void MoveToTill(BookDefinition book)
    {
        if (book.Item != null)
        {
            // hollow shelf move
        }
        else
        {
            Bookshelf.MoveToTill(book);   
        }

        if (TillBook != null)
        {
            var bookDefinition = TillBook.GetComponent<Book>().BookDefinition;

            if (bookDefinition.Item != null)
            {
                // hollow shelf put back
            }
            else
            {
                Bookshelf.PutBookBack(bookDefinition);
            }

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

    public bool IsBookAtTillWithItem(ItemDefinition item)
    {
        return TillBook != null && TillBook.GetComponent<Book>().BookDefinition.Item.Name == item.Name;
    }

    public bool IsBookAtTillWithSomeItem()
    {
        return TillBook != null && TillBook.GetComponent<Book>().BookDefinition.Item != null;
    }

    private void OnBookSell(GameEvent @event)
    {
        if (TillBook != null)
        {
            var book = TillBook.GetComponent<Book>().BookDefinition;

            if (book.IsHollow)
            {
                var b = HollowBooks.Find(bo => bo.Item.Name == book.Item.Name);
                RegisterHollowBookSale(b, @event.Character);

                HollowBookshelf.PlaceBooks(HollowBooks);
            }
            else
            {
                var b = Books.Find(b => b.IsEqual(book));
                RegisterBookSale(b, @event.Character);
                
                var booksInStock = Books.FindAll(InStock).ToList();
                Bookshelf.PlaceBooks(booksInStock);
            }

            Destroy(TillBook);

            TillView.Display(BankBalance);
        }
    }
}
