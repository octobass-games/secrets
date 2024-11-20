using UnityEngine;

[CreateAssetMenu]
public class StampDefinition : ScriptableObject
{
    public string Id;
    public bool IsUnlocked;

    public bool IsEqual(StampDefinition other)
    {
        return Id == other.Id;
    }
}
