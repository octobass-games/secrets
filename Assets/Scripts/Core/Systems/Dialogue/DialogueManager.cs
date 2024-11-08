using System.Linq;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private Dialogue Dialogue;
    public RequirementManager RequirementManager;

    public DialogueView ConversationView;
    public History History;

    private Line LineToSpeak;

    public void Begin(Dialogue dialogue)
    {
        Dialogue = dialogue;

        ConversationView.Open();
        LineToSpeak = Dialogue.Lines[0];
        SpeakLine();
    }

    public void End()
    {
        ConversationView.Close();
    }

    private void SpeakLine()
    {
        if (LineToSpeak.Events != null && LineToSpeak.Events.Count > 0)
        {
            EventManager eventManager = FindFirstObjectByType<EventManager>();

            LineToSpeak.Events.ForEach(e => eventManager.Publish(e));
        }

        var choices = LineToSpeak.Choices.FindAll(c => RequirementManager.AllSatisfied(c.Requirements));

        ConversationView.Display(LineToSpeak.Speaker, LineToSpeak.Text, choices, OnChoice);
    }

    private void OnChoice(Choice response)
    {
        bool wasChoiceless = response == null;

        if (wasChoiceless)
        {
            LineToSpeak = Dialogue.Lines.Find(l => l.Id == LineToSpeak.NextLine.Id);

            if (LineToSpeak != null)
            {
                SpeakLine();
            }
            else
            {
                End();
            }
        }
        else
        {
            if (response.Events != null && response.Events.Count > 0)
            {
                EventManager eventManager = FindFirstObjectByType<EventManager>();

                response.Events.ForEach(e => eventManager.Publish(e));
            }

            if (response.NextLine != null)
            {
                LineToSpeak = Dialogue.Lines.Find(l => l.Id == response.NextLine.Id);
                SpeakLine();
            }
            else
            {
                End();
            }
        }
    }
}
