using TMPro;
using UnityEngine;

public class BookshelfBook : MonoBehaviour
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
}
