using System.Linq;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private Dialogue Dialogue;

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
        var choices = LineToSpeak.Choices.FindAll(c => c.Requirements.All(r => History.Contains(r)));

        ConversationView.Display(LineToSpeak.Speaker, LineToSpeak.Text, choices, OnChoice);
    }

    private void OnChoice(Choice response)
    {
        bool wasChoiceless = response == null;

        if (wasChoiceless)
        {
            LineToSpeak = Dialogue.Lines.Find(l => l.Id == LineToSpeak.NextLineId);

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
            if (response.NextLineId != null)
            {
                LineToSpeak = Dialogue.Lines.Find(l => l.Id == response.NextLineId);
                SpeakLine();
            }
            else
            {
                End();
            }
        }
    }
}
