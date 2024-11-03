using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject PauseMenu;

    public void Pause()
    {
        Time.timeScale = 0;
        PauseMenu.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
    }
}
