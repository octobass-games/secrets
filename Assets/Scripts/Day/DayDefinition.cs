using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DayDefinition : ScriptableObject
{
    public string Date;
    public List<GameEvent> DailyEvents;
    public bool IsInThePast;
    public Line SupplierDialogue;

    public bool IsEqual(DayDefinition other)
    {
        return Date == other.Date;
    }
}
