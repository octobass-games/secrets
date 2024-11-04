using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterDefinition : ScriptableObject
{
    public string Name;
    public List<TextAsset> Interactions;
    public int Relationship;
}
