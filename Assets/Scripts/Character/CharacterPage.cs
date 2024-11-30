using TMPro;
using UnityEngine;

public class CharacterPage : MonoBehaviour
{
    public Character Character;

    public GameObject TidbitContainer;
    public GameObject TidbitPrefab;
    public TMPro.TextMeshProUGUI CharacterName;

    void OnEnable()
    {
        var tidbits = Character.GetUnlockedTidbits();
        if (tidbits.Count > 0)
        {
            CharacterName.text = Character.CharacterDefinition.Name;
        }

        for (int i = 0; i < tidbits.Count; i++)
        {
            var thing = Instantiate(TidbitPrefab);
            thing.transform.SetParent(TidbitContainer.transform);
            thing.transform.localScale = Vector3.one;
            thing.GetComponent<TMPro.TextMeshProUGUI>().text = tidbits[i].Tidbit;
        }
    }
}
