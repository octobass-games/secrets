using UnityEngine;

[CreateAssetMenu]
public class CharacterTidbit : ScriptableObject
{
    public string Id;
    [TextArea]
    public string Tidbit;
    public bool IsUnlocked;

    public bool IsEqual(CharacterTidbit other)
    {
        return Id == other.Id;
    }
}
