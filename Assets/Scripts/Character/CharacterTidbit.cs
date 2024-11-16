using UnityEngine;

[CreateAssetMenu]
public class CharacterTidbit : ScriptableObject
{
    public string Id;
    [TextArea]
    public string Tidbit;
    public bool IsUnlocked;
}
