using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DayEnderAnimation : MonoBehaviour
{
    public Bookkeeper Bookkeeper;
    public List<BookDefinition> books;
    public GameObject NotificationPrefab;
    public Transform NotificationParent;


    void Start()
    {
        var books = Bookkeeper
            .GetEndOfDayTransactions()
            .BookSales
            .SelectMany(b => {
                List<BookSale> bookSales = new();
            
                for (int i = 0; i < b.Quantity; i++)
                {
                    bookSales.Add(new BookSale(b.Name, b.SellPrice, 1));
                }

                return bookSales;
            })
            .ToList();

        for (int i = 0; i < books.Count; i++)
        {
            StartCoroutine(WaitForNSecondsThenSendFadeIn(i, books[i]));
        }
    }


    IEnumerator WaitForNSecondsThenSendFadeIn(float n, BookSale book)
    {
        yield return new WaitForSeconds(n);
        
        var item = Instantiate(NotificationPrefab, NotificationParent);

        item.GetComponentInChildren<TextMeshPro>().text = book.Name + "\n" + book.SellPrice + " coins";
        item.transform.localPosition = new Vector2(Random.Range(-200, 200) , 10);
    }
}
