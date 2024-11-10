using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject LoadButton;

    void Start()
    {
        if (!SaveManager.Instance.HasSaveData())
        {
            LoadButton.SetActive(false);
        }
    }

    public void NewGame()
    {
        SaveManager.Instance.DeleteSaveData();
        LoadShopScene();
    }

    public void LoadShopScene()
    {
        SceneManager.Instance.ChangeScene("Shop");
    }

    public void LoadCreditsScene()
    {
        SceneManager.Instance.ChangeScene("Credits");
    }
}
