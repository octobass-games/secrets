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

    public void Display(Line line, Action<Response> onResponse)
    {
        Debug.Log(line.Text);
        Speaker.text = line.Speaker;
        Line.text = line.Text;
        Response.onClick.AddListener(() => onResponse(line.Responses[0]));
        Response.GetComponentInChildren<TMP_Text>().text = line.Responses[0].Text;
    }
}
