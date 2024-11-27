using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DayEnderAnimation : MonoBehaviour
{
    public Bookkeeper Bookkeeper;
    public GameObject NotificationPrefab;
    public Transform NotificationParent;
    public GameObject Person;

    private IEnumerator SaleAnimation;

    public void Play()
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
            .Shuffle()
            .ToList();

        Person.SetActive(true);

        SaleAnimation = WaitForNSecondsThenSendFadeIn(books);

        StartCoroutine(SaleAnimation);
    }

    public void Stop()
    {
        Person.SetActive(false);
        StopCoroutine(SaleAnimation);
    }

    IEnumerator WaitForNSecondsThenSendFadeIn(List<BookSale> bookSales)
    {
        for (int i = 0; i < bookSales.Count; i++)
        {
            yield return new WaitForSeconds(0.75f);

            var book = bookSales[i];

            var item = Instantiate(NotificationPrefab, NotificationParent);

            item.GetComponentInChildren<TextMeshPro>().text = "Sold " + book.Name + "\n" + book.SellPrice + " coins";
            item.transform.localPosition = new Vector2(Random.Range(-200, 200), 10);
        }
    }
}
