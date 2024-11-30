using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public SaveManager SaveManager;
    public GameObject LoadButton;

    void Start()
    {
        if (!SaveManager.HasSaveData())
        {
            LoadButton.SetActive(false);
        }
    }

    public void NewGame()
    {
        SaveManager.DeleteSaveData();
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
