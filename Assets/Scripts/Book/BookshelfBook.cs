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
    public SpriteRenderer BookGhost;

    public void Setup(BookDefinition book, Action<BookDefinition> onPickup, Action<BookDefinition> onInspect)
    {
        BookDefinition = book;

        HoverDescription.text = book.Name;
        HoverCover.sprite = book.Cover;
        Book.color = book.Colour;
        BookGhost.color = new Color(book.Colour.r, book.Colour.g, book.Colour.b, BookGhost.color.a);
        
        Clickable.OnClick.AddListener(() => onPickup(book));
        Clickable.OnRightClick.AddListener(() => onInspect(book));
    }
}
