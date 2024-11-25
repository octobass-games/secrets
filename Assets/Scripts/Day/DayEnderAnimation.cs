using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DayEnderAnimation : MonoBehaviour
{
    public List<BookDefinition> books;
    public GameObject NotificationPrefab;


    void Start()
    {
        for (int i = 0; i < books.Count; i++)
        {
            StartCoroutine(WaitForNSecondsThenSendFadeIn(i, books[i]));
        }
    }


    IEnumerator WaitForNSecondsThenSendFadeIn(float n, BookDefinition book)
    {
        yield return new WaitForSeconds(n);
       var item = Instantiate(NotificationPrefab);
        item.GetComponentInChildren<TextMeshPro>().text = book.Name + "\n" + book.SellPrice + " coins";


        item.transform.position = new Vector2(Random.Range(-200, 200) , 10);
    }
}
