using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public Camera GameOverCamera;
    public List<Camera> OtherCameras;
    public TMP_Text Date;
    public TMP_Text Headline;

    void OnEnable()
    {
        EventManager.Instance.Subscribe(GameEventType.GAME_OVER, OnGameOver);
    }

    void OnDisable()
    {
        EventManager.Instance.Unsubscribe(GameEventType.GAME_OVER, OnGameOver);
    }

    private void OnGameOver(GameEvent @event)
    {
        Date.text = @event.Day.Date;
        Headline.text = @event.Message;

        OtherCameras.ForEach(camera => camera.gameObject.SetActive(false));
        
        GameOverCamera.gameObject.SetActive(true);
    }
}
