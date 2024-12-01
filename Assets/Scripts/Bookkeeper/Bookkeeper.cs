using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bookkeeper : MonoBehaviour, Savable
{
    public List<BookDefinition> Books;
    public List<ItemDefinition> Items;
    public TillView TillView;
    public Bookshelf Bookshelf;
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
    private int MonthlyRent;
    
    private System.Random RandomNumberGenerator = new System.Random();

    void Awake()
    {
        Books = Books.Select(b => Instantiate(b)).ToList();
        HollowBooks = HollowBooks.Select(b => Instantiate(b)).ToList();
        Items = Items.Select(i => Instantiate(i)).ToList();
    }

    public int CalculateTax()
    {
        int upperBound = DailyTransactions.Count - 2;
        int lowerBound = Mathf.Max(0, upperBound - 7);

        int totalEarnings = 0;

        for (int i = lowerBound; i <= upperBound; i++)
        {
            var dailyTransactions = DailyTransactions[i];

            var bookSalesEarnings = dailyTransactions.BookSales.Select(b => b.SellPrice * b.Quantity).Sum();
            var uniqueBookSalesEarnings = dailyTransactions.UniqueBookSales.Select(b => b.SellPrice).Sum();

            totalEarnings += bookSalesEarnings + uniqueBookSalesEarnings;
        }

        return Mathf.CeilToInt(totalEarnings * 0.125f);
    }

    public int GetMonthlyRent()
    {
        return MonthlyRent; 
    }

    void OnEnable()
    {
        EventManager.Instance.Subscribe(GameEventType.BEGIN_DAY, OnBeginDay);
        EventManager.Instance.Subscribe(GameEventType.CLOSE_SHOP, OnCloseShop);
        EventManager.Instance.Subscribe(GameEventType.RENT_PAYMENT, OnRentPayment);
        EventManager.Instance.Subscribe(GameEventType.TAX_PAYMENT, OnTaxPayment);
        EventManager.Instance.Subscribe(GameEventType.BOOK_ORDER, OnBookOrder);
        EventManager.Instance.Subscribe(GameEventType.ITEM_ORDER, OnItemOrder);
        EventManager.Instance.Subscribe(GameEventType.INVENTORY_SELL, OnBookSell);
        EventManager.Instance.Subscribe(GameEventType.PAYING_FOR_INFORMATION, OnPayingForInformation);
        EventManager.Instance.Subscribe(GameEventType.MONTHLY_RENT_AGREED, OnMonthlyRentAgreed);
        EventManager.Instance.Subscribe(GameEventType.BANK_DEPOSIT, OnBankDeposit);
        EventManager.Instance.Subscribe(GameEventType.BANK_WITHDRAWAL, OnBankWithdrawal);
        EventManager.Instance.Subscribe(GameEventType.VELIA_ITEM_RECEIVE, OnVeliaItemReceive);
    }

    void OnDisable()
    {
        EventManager.Instance.Unsubscribe(GameEventType.BEGIN_DAY, OnBeginDay);
        EventManager.Instance.Unsubscribe(GameEventType.CLOSE_SHOP, OnCloseShop);
        EventManager.Instance.Unsubscribe(GameEventType.RENT_PAYMENT, OnRentPayment);
        EventManager.Instance.Unsubscribe(GameEventType.TAX_PAYMENT, OnTaxPayment);
        EventManager.Instance.Unsubscribe(GameEventType.BOOK_ORDER, OnBookOrder);
        EventManager.Instance.Unsubscribe(GameEventType.ITEM_ORDER, OnItemOrder);
        EventManager.Instance.Unsubscribe(GameEventType.INVENTORY_SELL, OnBookSell);
        EventManager.Instance.Unsubscribe(GameEventType.PAYING_FOR_INFORMATION, OnPayingForInformation);
        EventManager.Instance.Unsubscribe(GameEventType.MONTHLY_RENT_AGREED, OnMonthlyRentAgreed);
        EventManager.Instance.Unsubscribe(GameEventType.BANK_DEPOSIT, OnBankDeposit);
        EventManager.Instance.Unsubscribe(GameEventType.BANK_WITHDRAWAL, OnBankWithdrawal);
        EventManager.Instance.Unsubscribe(GameEventType.VELIA_ITEM_RECEIVE, OnVeliaItemReceive);
    }

    private void OnVeliaItemReceive(GameEvent @event)
    {
        if (@event.Items.Count > 0)
        {
            foreach (var item in @event.Items)
            {
                var i = Items.Find(it => it.Name == item.Name);

                if (i != null)
                {
                    i.Stock++;
                }
            }
        }
    }

    private void OnBankDeposit(GameEvent @event)
    {
        Deposit(@event.Amount);
    }

    private void OnBankWithdrawal(GameEvent @event)
    {
        Withdraw(@event.Amount);
    }

    private void OnPayingForInformation(GameEvent @event)
    {
        TransactionsToday.MiscellaneousCosts += @event.Amount;

        Withdraw(@event.Amount);
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
        int rent = @event.Amount != 0 ? @event.Amount : MonthlyRent;

        TransactionsToday.Rent = rent;

        Withdraw(rent);
    }

    private void OnTaxPayment(GameEvent _)
    {
        int tax = CalculateTax();

        TransactionsToday.Tax = tax;

        Withdraw(tax);
    }

    public void OnBeginDay(GameEvent @event)
    {
        Today = @event.Day;

        TransactionsToday = new DailyTransactions(Today.Date, new(), new(), new(), new(), 0, 0, 0, 0);
        DailyTransactions.Add(TransactionsToday);

        var booksInStock = Books.FindAll(InStock).ToList();

        if (TillBook != null)
        {
            Destroy(TillBook);
        }

        Bookshelf.PlaceBooks(booksInStock, HollowBooks, null);
        TillView.DisplayImmediately(BankBalance);
    }

    private void OnMonthlyRentAgreed(GameEvent @event)
    {
        MonthlyRent = @event.Amount;
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
        BankBalance = Mathf.Max(0, BankBalance - amount);

        TillView.Display(BankBalance);
    }

    private void Deposit(int amount)
    {
        BankBalance += amount;

        TillView.Display(BankBalance);
    }

    public void UpdateBooks()
    {
        UpdateSalesRecords();
        UpdateStock();
        TransactionsToday.EndOfDayBankBalance = BankBalance;
    }

    public bool IsAffordablePayment(int paymentAmount)
    {
        return BankBalance >= paymentAmount;
    }

    private void OnBookOrder(GameEvent @event)
    {
        int totalOrderCost = 0;

        foreach (var book in @event.BooksToOrder)
        {
            var bookDefinition = Books.Find(b => b.IsEqual(book));

            var bookOrderCost = @event.Amount * bookDefinition.CostToOrder;

            TransactionsToday.BookOrders.Add(new Order(book.Name, @event.Amount, bookOrderCost));

            totalOrderCost += bookOrderCost;
        }

        Withdraw(totalOrderCost);
    }

    private void OnItemOrder(GameEvent @event)
    {
        int totalOrderCost = 0;

        foreach (var item in @event.ItemsToOrder)
        {
            var itemDefinition = Items.Find(i => i.IsEqual(item));

            var itemOrderCost = itemDefinition.SellPrice * @event.Amount;

            TransactionsToday.ItemOrders.Add(new Order(itemDefinition.Name, @event.Amount, itemOrderCost));

            totalOrderCost += itemOrderCost;
        }

        Withdraw(totalOrderCost);
    }

    private void UpdateSalesRecords()
    {
        foreach (BookDefinition book in Books)
        {
            float likelihoodOfBookSale = CalculateLikelihoodOfSale(book);

            int numberToTry = book.Stock / 20;

            for (int i = 1; i < numberToTry; i++)
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
        if (book.SellPrice > 0) {
            book.SellPrice--;
        }
    }

    public bool InStock(BookDefinition book)
    {
        return Books.Find(b => b.IsEqual(book)).Stock > 0;
    }

    public bool CanHollow()
    {
        if (HollowBooks.Count >= 3)
        {
            DialogueManager.Begin(TooManyHollowBooks, null, null);

            return false;
        }

        return true;
    }

    public BookDefinition HollowBook(BookDefinition book)
    {
        if (!CanHollow())
        {
            return null;
        }

        var b = Books.Find(b => b.IsEqual(book));
        b.Stock--;

        var hollow = Instantiate(book);

        HollowBooks.Add(hollow);

        var booksInStock = Books.FindAll(InStock).ToList();
        hollow.IsHollow = true;
        Bookshelf.PlaceBooks(booksInStock, HollowBooks, TillBook);

        return hollow;
    }

    public void InsertIntoHollowBook(BookDefinition hollowBook, ItemDefinition item)
    {
        hollowBook.Item = item;
        var booksInStock = Books.FindAll(InStock).ToList();

        Bookshelf.PlaceBooks(booksInStock, HollowBooks, TillBook);
    }

    private void UpdateStock()
    {
        foreach (BookDefinition book in Books)
        {
            var bookOrder = TransactionsToday.BookOrders.Find(b => b.Name ==  book.Name);

            if (bookOrder != null)
            {
                book.Stock += bookOrder.Quantity;
            }
        }

        foreach (ItemDefinition item in Items)
        {
            var itemOrder = TransactionsToday.ItemOrders.Find(i => i.Name == item.Name);

            if (itemOrder != null)
            {
                item.Stock += itemOrder.Quantity;
            }
        }
    }

    private void RegisterBookSale(BookDefinition book, CharacterDefinition character, int customAmount = 0)
    {
        book.Stock--;

        if (character == null)
        {
            BankBalance += book.SellPrice;
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
            var price = customAmount != 0 ? customAmount : book.RecommendedSellPrice * 3;

            BankBalance += price;

            TransactionsToday.UniqueBookSales.Add(new UniqueBookSale(character.Name, book.Name, price));
        }
    }

    private void RegisterHollowBookSale(BookDefinition book, CharacterDefinition character, int customAmount, bool sellForFree)
    {
        var price = customAmount != 0 ? customAmount : sellForFree ? 0 : book.Item.SellPrice;
        
        BankBalance += price;

        TransactionsToday.UniqueBookSales.Add(new UniqueBookSale(character.Name, book.Name, price));

        var b = HollowBooks.Find(b => b.Item.Name == book.Item.Name);

        var i = Items.Find(item => item.Name == b.Item.Name);

        i.Stock--;

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
        var itemsData = Items.Select(i => new ItemData(i.Name, i.Stock)).ToList();

        saveData.Bookkeeper = new BookkeeperData(BankBalance, booksData, itemsData, hollowBooksData, DailyTransactions, MonthlyRent);
    }

    public bool CanAffordRent()
    {
        return BankBalance >= MonthlyRent;
    }

    public void Load(SaveData saveData)
    {
        BookkeeperData bookkeeperData = saveData.Bookkeeper;
        List<BookData> booksData = bookkeeperData.Books;
        List<BookData> hollowBooksData = bookkeeperData.HollowBooks;
        List<ItemData> items = bookkeeperData.Items;

        BankBalance = bookkeeperData.BankBalance;
        DailyTransactions = bookkeeperData.DailyTransactions;
        MonthlyRent = bookkeeperData.MonthlyRent;
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

            hollowBook.IsHollow = true;

            if (hollowBookData.Item != null)
            {
                var item = Items.Find(item => item.Name == hollowBookData.Item);

                hollowBook.Item = item;
            }

            return hollowBook;
        }).ToList();
        Items.ForEach(i =>
        {
            var itemData = items.Find(item => i.Name == item.Name);

            if (itemData != null)
            {
                i.Stock = itemData.Stock;
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
        return TillBook != null && TillBook.GetComponent<Book>().BookDefinition.IsEqual(book) && !TillBook.GetComponent<Book>().BookDefinition.IsHollow;
    }

    public bool IsBookAtTillWithItem(ItemDefinition item)
    {
        if (TillBook != null)
        {
            var itemInBook = TillBook.GetComponent<Book>().BookDefinition.Item;

            return itemInBook != null && itemInBook.Name == item.Name;
        }

        return false;
    }

    public bool IsBookAtTillWithSomeItem()
    {
        return TillBook != null && TillBook.GetComponent<Book>().BookDefinition.Item != null;
    }

    public bool CanAffordTaxes()
    {
        return BankBalance >= CalculateTax();
    }

    private void OnBookSell(GameEvent @event)
    {
        if (TillBook != null)
        {
            var book = TillBook.GetComponent<Book>().BookDefinition;

            if (book.IsHollow)
            {
                var b = HollowBooks.Find(bo => bo.Item.Name == book.Item.Name && bo.Name == book.Name);

                RegisterHollowBookSale(b, @event.Character, @event.Amount, @event.SellForFree);
                var booksInStock = Books.FindAll(InStock).ToList();

                Bookshelf.PlaceBooks(booksInStock, HollowBooks, null);
            }
            else
            {
                var b = Books.Find(b => b.IsEqual(book));
                RegisterBookSale(b, @event.Character, @event.Amount);
                
                var booksInStock = Books.FindAll(InStock).ToList();
                Bookshelf.PlaceBooks(booksInStock, HollowBooks, null);
            }

            Destroy(TillBook);

            TillView.Display(BankBalance);
        }
    }
}
