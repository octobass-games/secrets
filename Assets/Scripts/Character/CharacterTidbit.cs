using UnityEngine;

[CreateAssetMenu]
public class CharacterTidbit : ScriptableObject
{
    [TextArea]
    public string Tidbit;
    public bool Unlocked;
}
