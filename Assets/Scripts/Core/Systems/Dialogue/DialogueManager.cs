using System;
using System.Linq;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public RequirementManager RequirementManager;

    public DialogueView ConversationView;
    public History History;

    private Line LineToSpeak;
    private Animator SpeakerAnimator;

    private Action OnEnd;

    public void Begin(Line root, Animator speakerAnimator, Action onEnd)
    {
        SpeakerAnimator = speakerAnimator;
        ConversationView.Open();
        LineToSpeak = root;
        OnEnd = onEnd;
        SpeakLine();
    }

    public void Begin(Line root)
    {
        Begin(root, null, null);
    }

    public void End()
    {
        ConversationView.Close();

        if (OnEnd != null)
        {
            OnEnd();
            OnEnd = null;
        }
    }

    private void SpeakLine()
    {
        var choices = LineToSpeak.Choices.FindAll(c => RequirementManager != null && RequirementManager.AllSatisfied(c.Requirements));

        ConversationView.Display(LineToSpeak.Speaker.Name, LineToSpeak.Text, choices, OnChoice, SpeakerAnimator);
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
            RunLineEvents();

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
