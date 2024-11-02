using System.Collections.Generic;
using UnityEngine;

public class Conversable : MonoBehaviour
{
    private Dialogue Dialogue = new Dialogue(
        new List<Line> {
            new("0", "Speaker", "How are you?", null, new List<Choice>
            {
                new("Hi", "", "1")
            }),
            new("1", "Speaker", "How are you today?", null, new List<Choice>
            {
                new("Hi again", "", "2")
            }),
            new("2", "Speaker", "How are you today again?", "3", null),
            new("3", "Speaker", "How are you today again again?", null, null)
        }
    );

    public ConversationView ConversationView;

    private Line LineToSpeak;

    public void Begin()
    {
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
