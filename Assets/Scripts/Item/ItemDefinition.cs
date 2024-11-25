using UnityEngine;

[CreateAssetMenu]
public class ItemDefinition : ScriptableObject
{
    public string Name;

    public bool IsEqual(ItemDefinition other)
    {
        return Name == other.Name;
    }
}
