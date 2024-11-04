using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueView : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject ResponsePrefab;
    public RectTransform ResponseSpawn;

    public TMP_Text Speaker;
    public TMP_Text Line;

    private List<GameObject> Responses = new();

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
        Speaker.text = speaker;
        Line.text = text;

        foreach (GameObject response in Responses)
        {
            response.GetComponentInChildren<Button>().onClick.RemoveAllListeners();

            Destroy(response);
        }

        if (choices.Count > 0)
        {
            for (int i = 0; i < choices.Count; i++)
            {
                Choice choice = choices[i];

                GameObject go = Instantiate(ResponsePrefab, ResponseSpawn);

                go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y + 70 * i, go.transform.position.z);
                go.GetComponentInChildren<TMP_Text>().text = choice.Text;
                go.GetComponentInChildren<Button>().onClick.AddListener(() => onChoice(choice));

                Responses.Add(go);
            }
        }
        else
        {
            GameObject go = Instantiate(ResponsePrefab, ResponseSpawn);

            go.GetComponentInChildren<TMP_Text>().text = "Next";
            go.GetComponentInChildren<Button>().onClick.AddListener(() => onChoice(null));

            Responses.Add(go);
        }
    }
}
