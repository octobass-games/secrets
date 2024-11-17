using UnityEngine;

public class Supplier : MonoBehaviour
{
    public DayManager DayManager;
    public DialogueManager DialogueManager;
    public Line NoAnswer;

    public void Call()
    {
        DialogueManager.Begin(DayManager.GetSupplierInteraction().RootLine, null);
    }
}
