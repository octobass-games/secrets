using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private Dialogue Dialogue;

    public DialogueView ConversationView;

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
        ConversationView.Display(LineToSpeak, OnChoice, OnNext);
    }

    private void OnNext()
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

    private void OnChoice(Choice response)
    {
        if (response.LineId != null)
        {
            LineToSpeak = Dialogue.Lines.Find(l => l.Id == response.LineId);
            SpeakLine();
        }
        else
        {
            End();
        }
    }
}
