using UnityEngine;

public class SignFlasher : MonoBehaviour
{
    public GameObject SignFlasherGameObject;

    public DayManager DayManager;

    void Update()
    {
        if (DayManager.IsNextEventType(GameEventType.OPEN_SHOP))
        {
            OnNextEventShopOpenOrClose();
        }
        if (DayManager.IsNextEventType(GameEventType.CLOSE_SHOP))
        {
            OnNextEventShopOpenOrClose();
        }
    }

    void OnEnable()
    {
        EventManager.Instance.Subscribe(GameEventType.OPEN_SHOP, OnEvent);
        EventManager.Instance.Subscribe(GameEventType.CLOSE_SHOP, OnEvent);
    }

    void OnDisable()
    {
        EventManager.Instance.Unsubscribe(GameEventType.OPEN_SHOP, OnEvent);
        EventManager.Instance.Unsubscribe(GameEventType.CLOSE_SHOP, OnEvent);
    }

    public void OnEvent(GameEvent @event)
    {
        SignFlasherGameObject.SetActive(false);
    }

    private void OnNextEventShopOpenOrClose()
    {
        SignFlasherGameObject.SetActive(true);
    }
}
