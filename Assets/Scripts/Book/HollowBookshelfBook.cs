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
        if (bookDefinition.Item != null )
        {
            HoverCover.sprite = bookDefinition.Item.Sprite;
            HoverDescription.text = bookDefinition.Item.Name;
        }
        else
        {
            HoverCover.sprite = bookDefinition.Cover;
            HoverDescription.text = bookDefinition.Name;
        }
    }
}
