using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu]
public class CharacterDefinition : ScriptableObject
{
    public string Name;
    public List<Interaction> Interactions;
    public int Relationship;
    public Sprite Profile;
    public AnimatorController Animator;
    public CharacterTidbit[] Tidbits;
}
