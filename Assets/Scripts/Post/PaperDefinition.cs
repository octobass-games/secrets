using UnityEngine;

[CreateAssetMenu]
public class PaperDefinition : ScriptableObject
{
    public string Name;
    public DayDefinition DayDefinition;

    public bool IsEqual(PaperDefinition other)
    {
        return Name == other.Name;
    }
}
