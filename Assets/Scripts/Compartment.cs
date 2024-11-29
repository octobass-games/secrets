using UnityEngine;

public class Compartment : MonoBehaviour
{
    public History History;
    public DialogueManager DialogueManager;
    public Line HiddenCompartmentFoundDialogue;

    void Start()
    {
        if (!History.Contains("secret.compartment.discussed"))
        {
            DialogueManager.Begin(HiddenCompartmentFoundDialogue);
        }
    }
}
