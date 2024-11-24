using UnityEngine;

public class DayEnder : MonoBehaviour
{
    public Camera ShopCamera;
    public Camera InventoryCamera;
    public GameObject DayEndOverlay;

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
        ShopCamera.gameObject.SetActive(false);
        InventoryCamera.gameObject.SetActive(true);
        DayEndOverlay.SetActive(true);
    }
}
