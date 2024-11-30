using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public SaveManager SaveManager;
    public SceneManager SceneManager;
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
        SceneManager.ChangeScene("Shop");
    }

    public void LoadCreditsScene()
    {
        SceneManager.ChangeScene("Credits");
    }
}
