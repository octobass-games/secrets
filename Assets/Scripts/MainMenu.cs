using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public SaveManager SaveManager;
    public SceneManager SceneManager;
    public GameObject LoadButton;
    public GameObject QuitButton;

    void Start()
    {
        if (!SaveManager.HasSaveData())
        {
            LoadButton.SetActive(false);
        }

        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            QuitButton.SetActive(false);
        }
    }

    public void NewGame()
    {
        SaveManager.DeleteSaveData();
        LoadOpeningScene();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadShopScene()
    {
        SceneManager.ChangeScene("Shop");
    }


    public void LoadOpeningScene()
    {
        SceneManager.ChangeScene("Opening");
    }


    public void LoadCreditsScene()
    {
        SceneManager.ChangeScene("Credits");
    }
}
