using System.Collections.Generic;
using UnityEngine;

public class Conversable : MonoBehaviour
{
    private Dialogue Dialogue = new Dialogue(
        new List<Line> {
            new("0", "Speaker", "How are you?", new List<Response>
            {
                new("Hi", "", "1")
            }),
            new("1", "Speaker", "How are you today?", new List<Response>
            {
                new("Hi again", "", null)
            })
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
        ConversationView.Display(LineToSpeak, OnResponse);
    }

    private void OnResponse(Response response)
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
