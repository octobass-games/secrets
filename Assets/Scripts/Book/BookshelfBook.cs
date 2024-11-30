using System;
using TMPro;
using UnityEngine;

public class BookshelfBook : MonoBehaviour
{
    public BookDefinition BookDefinition;
    public Clickable Clickable;
    public SpriteRenderer Book;
    public SpriteRenderer HoverCover;
    public TMP_Text HoverDescription;
    public TMP_Text HoverStock;
    public SpriteRenderer BookGhost;

    public void Setup(BookDefinition book, Action<BookDefinition> onPickup, Action<BookDefinition> onInspect)
    {
        BookDefinition = book;

        HoverDescription.text = book.Name;
        HoverStock.text = "Stock: " +book.Stock.ToString();
        HoverCover.sprite = book.Cover;

        if (book.Item != null)
        {
            HoverCover.sprite = book.Item.Sprite;
            HoverDescription.text = book.Item.Name;
            HoverStock.text = "";
            HoverCover.transform.localScale = new Vector2(0.3f, 0.3f);
        }
        else
        {
            HoverCover.sprite = book.Cover;
            HoverDescription.text = book.Name;
            HoverCover.transform.localScale = new Vector2(1, 1);

        }

        Book.color = book.Colour;
        BookGhost.color = new Color(book.Colour.r, book.Colour.g, book.Colour.b, BookGhost.color.a);
        
        Clickable.OnClick.AddListener(() => onPickup(book));
        Clickable.OnRightClick.AddListener(() => onInspect(book));
    }
}
