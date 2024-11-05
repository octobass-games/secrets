using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu]
public class CharacterDefinition : ScriptableObject
{
    public string Name;
    public List<TextAsset> Interactions;
    public int Relationship;
    public Sprite Profile;
    public AnimatorController Animator;
}
