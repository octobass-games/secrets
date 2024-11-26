using TMPro;
using UnityEngine;

public class HollowBookshelfBook : MonoBehaviour
{
    public Clickable Clickable;
    public SpriteRenderer HoverCover;
    public TMP_Text HoverDescription;

    public void Setup(BookDefinition bookDefinition, Bookkeeper bookkeeper)
    {
        Clickable.OnClick.AddListener(() => bookkeeper.MoveToTill(bookDefinition));
        HoverCover.sprite = bookDefinition.Cover;
        HoverDescription.text = bookDefinition.Name;
    }
}
