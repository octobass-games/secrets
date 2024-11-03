using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueView : MonoBehaviour
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

    public void Display(string speaker, string text, List<Choice> choices, Action<Choice> onChoice)
    {
        Response.onClick.RemoveAllListeners();

        Speaker.text = speaker;
        Line.text = text;

        if (choices.Count > 0)
        {
            Response.onClick.AddListener(() => onChoice(choices[0]));
        }
        else
        {
            Response.onClick.AddListener(() => onChoice(null));
        }

        Response.GetComponentInChildren<TMP_Text>().text = choices != null && choices.Count > 0 ? choices[0].Text : "Next";
    }
}
