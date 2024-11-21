using UnityEngine;

[CreateAssetMenu]
public class BookDefinition : ScriptableObject
{
    public string Name;
    public string Author;
    public string Description;
    public BookCategory Category;
    public Sprite Sprite;
    public Color colour;
    public int RecommendedSellPrice;
    public int SellPrice;
    public int Stock;
    public int CostToOrder;
    public GameObject Item;
    public float LikelihoodToSell;
    public float BaseLikelihoodToSell;

    public bool IsEqual(BookDefinition other)
    {
        return Name == other.Name;
    }
}
