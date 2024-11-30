using UnityEngine;

[CreateAssetMenu]
public class StampDefinition : ScriptableObject
{
    public string Name;
    public bool IsUnlocked;
    public string Description;
    public Sprite Image;

    public bool IsEqual(StampDefinition other)
    {
        return Name == other.Name;
    }
}
