using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class BookDefinition : ScriptableObject
{
    public string Name;
    public string Author;
    public string Description;
    public string ISBN;
    public BookCategory Category;
    [FormerlySerializedAs("Sprite")]
    public Sprite Cover;
    [FormerlySerializedAs("colour")]
    public Color Colour;
    public int RecommendedSellPrice;
    public int SellPrice;
    public int Stock;
    public int CostToOrder;
    public ItemDefinition Item;
    public float LikelihoodToSell;
    public float BaseLikelihoodToSell;
    public bool IsHollow;

    public bool IsEqual(BookDefinition other)
    {
        return Name == other.Name;
    }
}
