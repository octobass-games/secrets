using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public Camera GameOverCamera;
    public List<Camera> OtherCameras;
    public TMP_Text Date;
    public TMP_Text Headline;
    public DayManager DayManager;
    public Animator Fade;

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
        Fade.SetTrigger("out");
        Date.text = DayManager.GetToday().Date;
        Headline.text = @event.Message;     
        StartCoroutine(WaitForNSecondsThenSendFadeIn());
    }

    IEnumerator WaitForNSecondsThenSendFadeIn()
    {
        yield return new WaitForSeconds(1);
        OtherCameras.ForEach(camera => camera.gameObject.SetActive(false));

        GameOverCamera.gameObject.SetActive(true);
        Fade.SetTrigger("in");

    }
}
