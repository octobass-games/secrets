using Unity.VisualScripting;
using UnityEngine;

public class Stamp : MonoBehaviour
{
    public StampDefinition StampDefinition;
    public GameObject StampUnlocked;
    public GameObject StampLocked;
    public TMPro.TextMeshProUGUI Name;
    public TMPro.TextMeshProUGUI Description;

    void Awake()
    {
        Name.text = StampDefinition.Name;
        Description.text = StampDefinition.Description;
    }

    public void ShowUnlocked()
    {
        StampLocked.SetActive(false);
        StampUnlocked.SetActive(true);
    }
    public void ShowLocked()
    {
        StampLocked.SetActive(true);
        StampUnlocked.SetActive(false);
    }
}
