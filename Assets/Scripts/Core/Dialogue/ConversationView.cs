using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConversationView : MonoBehaviour
{
    public GameObject Canvas;

    public TMP_Text Speaker;
    public TMP_Text Line;
    public Button Response;

    public void Open()
    {
        Canvas.SetActive(true);
    }

    public void Close()
    {
        Canvas.SetActive(false);
    }

    public void Display(Line line, Action<Choice> onChoice, Action onNext)
    {
        Response.onClick.RemoveAllListeners();

        Speaker.text = line.Speaker;
        Line.text = line.Text;


        if (line.Choices != null)
        {
            Response.onClick.AddListener(() => onChoice(line.Choices[0]));
        }
        else
        {
            Response.onClick.AddListener(() => onNext());
        }

        Response.GetComponentInChildren<TMP_Text>().text = line.Choices != null ? line.Choices[0].Text : "Next";
    }
}
