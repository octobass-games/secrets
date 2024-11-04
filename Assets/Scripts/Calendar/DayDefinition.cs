using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DayDefinition : ScriptableObject
{
    public string Date;
    public List<CharacterDefinition> VisitingCharacters;
    public bool IsInThePast;
}
