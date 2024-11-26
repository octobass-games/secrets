using UnityEngine;

[CreateAssetMenu]
public class ItemDefinition : ScriptableObject
{
    public string Name;
    public int Stock;
    public Sprite Sprite;

    public bool IsEqual(ItemDefinition other)
    {
        return Name == other.Name;
    }
}
