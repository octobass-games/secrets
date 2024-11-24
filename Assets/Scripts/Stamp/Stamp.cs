using UnityEngine;

public class Stamp : MonoBehaviour
{
    public StampDefinition StampDefinition;
    public GameObject StampUnlocked;
    public GameObject StampLocked;

    public void ShowUnlocked()
    {
        StampUnlocked.SetActive(true);
        StampUnlocked.SetActive(false);
    }
    public void ShowLocked()
    {
        StampUnlocked.SetActive(false);
        StampUnlocked.SetActive(true);
    }
}
