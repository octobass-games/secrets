using UnityEngine;

[CreateAssetMenu]
public class CharacterDefinition : ScriptableObject
{
    public string Name;
    public string[] DialoguePaths;
    public int Relationship;
}
