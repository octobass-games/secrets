using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterDefinition : ScriptableObject
{
    public string Name;
    public List<Interaction> Interactions;
    public int Relationship;
    public Sprite Profile;
    public List<CharacterTidbit> Tidbits;
    public Interaction Confrontation;
    public Interaction ConfrontationWrong;

    public bool IsEqual(CharacterDefinition other)
    {
        return Name == other.Name;
    }
}
