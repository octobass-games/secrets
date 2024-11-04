using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DayDefinition : ScriptableObject
{
    public string Date;
    public List<string> VisitingCharacters;
    public bool IsInThePast;
}
