using System.Collections;
using UnityEngine;

public class DayEnder : MonoBehaviour
{
    public Camera ShopCamera;
    public Camera InventoryCamera;
    public GameObject DayEndOverlay;
    public Animator Fade;

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
    }

    IEnumerator WaitForNSecondsThenSendFadeIn()
    {
        yield return new WaitForSeconds(2);
        DayEndOverlay.SetActive(true);
        ShopCamera.gameObject.SetActive(false);
        InventoryCamera.gameObject.SetActive(true);

        Fade.SetTrigger("in");
    }
}
