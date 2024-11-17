using UnityEngine;

public class Supplier : MonoBehaviour
{
    public DayManager DayManager;
    public DialogueManager DialogueManager;
    public Line NoAnswerDialogue;

    private bool HasSpokenToSupplier;

    void Start()
    {
        EventManager.Instance.Subscribe(GameEventType.BEGIN_DAY, OnBeginDay);
    }

    private void OnBeginDay(GameEvent _)
    {
        HasSpokenToSupplier = false;
    }

    public void Call()
    {
        if (!HasSpokenToSupplier)
        {
            HasSpokenToSupplier = true;
            DialogueManager.Begin(DayManager.GetSupplierDialogue(), null);
        }
        else
        {
            DialogueManager.Begin(NoAnswerDialogue, null);
        }
    }
}
