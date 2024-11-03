using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterDefinition : ScriptableObject
{
    public string Name;
    public List<TextAsset> Dialogues;
    public int Relationship;
}
