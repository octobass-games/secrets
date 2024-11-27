using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterPage : MonoBehaviour
{
    public Character Character;

    public List<TMP_Text> Tidbits;

    void OnEnable()
    {
        var tidbits = Character.GetUnlockedTidbits();

        for (int i = 0; i < tidbits.Count; i++)
        {
            var tidbit = tidbits[i].Tidbit;

            if (i < Tidbits.Count)
            {
                Tidbits[i].text = tidbit; 
            }
        }
    }
}
