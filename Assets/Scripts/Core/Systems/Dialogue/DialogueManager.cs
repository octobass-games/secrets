using System.Linq;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public RequirementManager RequirementManager;

    public DialogueView ConversationView;
    public History History;

    private Line LineToSpeak;

    public void Begin(Line root)
    {
        ConversationView.Open();
        LineToSpeak = root;
        SpeakLine();
    }

    public void End()
    {
        ConversationView.Close();
    }

    private void SpeakLine()
    {
        var choices = LineToSpeak.Choices.FindAll(c => RequirementManager.AllSatisfied(c.Requirements));

        ConversationView.Display(LineToSpeak.Speaker.Name, LineToSpeak.Text, choices, OnChoice);
    }

    private void RunLineEvents()
    {
        if (LineToSpeak.Events != null && LineToSpeak.Events.Count > 0)
        {
            EventManager eventManager = FindFirstObjectByType<EventManager>();

            LineToSpeak.Events.ForEach(e => eventManager.Publish(e));
        }
    }

    private void OnChoice(Choice response)
    {
        bool wasChoiceless = response == null;

        if (wasChoiceless)
        {
            RunLineEvents();
            LineToSpeak = LineToSpeak.NextLine;

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
                LineToSpeak = response.NextLine;
                SpeakLine();
            }
            else
            {
                End();
            }
        }
    }
}
