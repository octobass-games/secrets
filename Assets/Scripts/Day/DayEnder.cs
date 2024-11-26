using System.Collections;
using UnityEngine;

public class DayEnder : MonoBehaviour
{
    public Camera ShopCamera;
    public Camera InventoryCamera;
    public Camera DayEndCamera;
    public DayEnderAnimation DayEnd;
    public Animator Fade;
    public GameObject Logbook;

    void OnEnable()
    {
        EventManager.Instance.Subscribe(GameEventType.END_DAY, OnEndDay);
    }

    void OnDisable()
    {
        EventManager.Instance.Unsubscribe(GameEventType.END_DAY, OnEndDay);
    }

    private void OnEndDay(GameEvent @event)
    {
        Fade.SetTrigger("out");
        StartCoroutine(WaitForNSecondsThenSendFadeIn());
        StartCoroutine(WaitForNSecondsThenSendLoadLog(5));
    }

    IEnumerator WaitForNSecondsThenSendFadeIn()
    {
        yield return new WaitForSeconds(2);
        DayEnd.Play();
        ShopCamera.gameObject.SetActive(false);
        InventoryCamera.gameObject.SetActive(false);
        DayEndCamera.gameObject.SetActive(true);

        Fade.SetTrigger("in");
    }

    IEnumerator WaitForNSecondsThenSendLoadLog(int time)
    {
        yield return new WaitForSeconds(time);
        Fade.SetTrigger("out");
        DayEnd.Stop();
        ShopCamera.gameObject.SetActive(true);
        InventoryCamera.gameObject.SetActive(false);
        DayEndCamera.gameObject.SetActive(false);
        Fade.SetTrigger("in");
        Logbook.SetActive(true);
    }
}
