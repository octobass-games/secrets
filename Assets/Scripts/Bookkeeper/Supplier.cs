using UnityEngine;

public class Supplier : MonoBehaviour
{
    public DayManager DayManager;
    public DialogueManager DialogueManager;
    public Line NoAnswerDialogue;

    public SpriteRenderer Phone;
    public GameObject PhoneUpOutline;
    public GameObject PhoneDownOutline;
    public Sprite PhoneUp;
    public Sprite PhoneDown;

    private bool HasSpokenToSupplier;

    void OnEnable()
    {
        EventManager.Instance.Subscribe(GameEventType.BEGIN_DAY, OnBeginDay);
    }

    void OnDisable()
    {
        EventManager.Instance.Unsubscribe(GameEventType.BEGIN_DAY, OnBeginDay);
    }

    private void OnBeginDay(GameEvent _)
    {
        HasSpokenToSupplier = false;
    }

    public void Call()
    {
        Line dialogue = DayManager.GetSupplierDialogue();

        PickPhoneUp();

        if (!HasSpokenToSupplier && dialogue != null)
        {
            DialogueManager.Begin(dialogue, null, PutPhoneDown);
            HasSpokenToSupplier = true;
        }
        else
        {
            DialogueManager.Begin(NoAnswerDialogue, null, PutPhoneDown);
        }
    }

    private void PickPhoneUp()
    {
        Phone.sprite = PhoneUp;

        PhoneUpOutline.SetActive(false);
        PhoneDownOutline.SetActive(false);
    }

    private void PutPhoneDown()
    {
        Phone.sprite = PhoneDown;
    }
}
