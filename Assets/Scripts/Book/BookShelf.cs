
using System.Collections.Generic;
using UnityEngine;

class BookShelf : MonoBehaviour
{
    public List<Book> books;

    public GameObject ShelfOne;
    public GameObject ShelfTwo;
    public GameObject ShelfThree;

    public void RenderShelves()
    {
        ClearAllShelves();


    }

    private void ClearAllShelves()
    {
        foreach (Transform child in ShelfOne.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in ShelfTwo.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in ShelfThree.transform)
        {
            Destroy(child.gameObject);
        }
    }


}