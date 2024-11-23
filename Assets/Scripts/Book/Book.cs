using UnityEngine;

public class Book : MonoBehaviour
{
    public BookDefinition BookDefinition;
    public SpriteRenderer SpriteRenderer;

    public void Setup()
    {
        SpriteRenderer.sprite = BookDefinition.Cover;
    }
}
