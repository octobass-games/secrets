using UnityEngine;

public class Supplier : MonoBehaviour
{
    public DayManager DayManager;
    public DialogueManager DialogueManager;
    public Line NoAnswerDialogue;

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
        
        if (!HasSpokenToSupplier && dialogue != null)
        {
            DialogueManager.Begin(dialogue, null);
            HasSpokenToSupplier = true;
        }
        else
        {
            DialogueManager.Begin(NoAnswerDialogue, null);
        }
    }
}
