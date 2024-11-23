using TMPro;
using UnityEngine;

public class Book : MonoBehaviour
{
    public BookDefinition BookDefinition;
    public SpriteRenderer HoverCover;
    public TMP_Text HoverDescription;

    void Awake()
    {
        BookDefinition = Instantiate(BookDefinition);
        HoverDescription.text = BookDefinition.Name;
        HoverCover.sprite = BookDefinition.Cover;
    }

    public bool IsCalled(string name)
    {
        return BookDefinition.Name == name;
    }

    public void InsertIntoBook(GameObject item)
    {
        BookDefinition.Item = item;
    }

    public void RemoveItemFromBook()
    {
        BookDefinition.Item = null;
    }
}
