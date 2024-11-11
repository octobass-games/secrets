using UnityEngine;

[CreateAssetMenu]
public class BookDefinition : ScriptableObject
{
    public string Name;
    public int RecommendedSellPrice;
    public int SellPrice;
    public int Stock;
    public int CostToOrder;
    public GameObject Item;
    public float LikelihoodToSell;
    public float BaseLikelihoodToSell;

    public Sprite Sprite;
    public Color colour;
}
