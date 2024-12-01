using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPage : MonoBehaviour
{
    public Character Character;

    public GameObject TidbitContainer;
    public GameObject TidbitPrefab;
    public TMPro.TextMeshProUGUI CharacterName;
    public bool ConfrontMode;
    public ConfrontationManager ConfrontationManager;


    void OnEnable()
    {
        var tidbits = Character.GetUnlockedTidbits();
        if (tidbits.Count > 0)
        {
            CharacterName.text = Character.CharacterDefinition.Name;
        }


        int numChildren = TidbitContainer.transform.childCount;
        for (int i = numChildren - 1; i >= 0; i--)
        {
            GameObject.Destroy(TidbitContainer.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < tidbits.Count; i++)
        {
            var tidbit = tidbits[i];
            var thing = Instantiate(TidbitPrefab);
            thing.transform.SetParent(TidbitContainer.transform);
            thing.transform.localScale = Vector3.one;
            thing.GetComponent<TMPro.TextMeshProUGUI>().text = tidbit.Tidbit;
            if (ConfrontMode)
            {
                thing.GetComponent<Button>().onClick.RemoveAllListeners();
                thing.GetComponent<Button>().onClick.AddListener(() => ConfrontationManager.SetLastPickedTidbit(tidbit));
            }
        }
    }
}
