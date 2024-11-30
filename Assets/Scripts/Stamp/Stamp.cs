using UnityEngine;
using UnityEngine.UI;

public class Stamp : MonoBehaviour
{
    public StampDefinition StampDefinition;
    public GameObject StampUnlocked;
    public GameObject StampLocked;
    public TMPro.TextMeshProUGUI Name;
    public TMPro.TextMeshProUGUI Description;
    public Image Image;
    public Sprite DefaultImage;

    void Awake()
    {
        Name.text = StampDefinition.Name;
        Description.text = StampDefinition.Description;
    }

    public void ShowUnlocked()
    {
        StampLocked.SetActive(false);
        StampUnlocked.SetActive(true);
        Image.sprite = StampDefinition.Image;

    }
    public void ShowLocked()
    {
        StampLocked.SetActive(true);
        StampUnlocked.SetActive(false);
        Image.sprite = DefaultImage;

    }
}
