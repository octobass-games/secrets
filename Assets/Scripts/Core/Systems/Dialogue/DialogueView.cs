using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueView : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject ResponsePrefab;
    public RectTransform ResponseSpawn;
    public GameObject DarkOverlay;
    public VerticalLayoutGroup ChoicesContainer;

    public TMP_Text Speaker;
    public TMP_Text Line;

    private List<GameObject> Responses = new();
    private WaitForSecondsRealtime Timer;
    private IEnumerator TypeWritingCoroutine;

    private string LineToWrite;
    private List<Choice> Choices;
    private Action<Choice> OnChoice;

    private bool IsWriting = false;

    private Animator SpeakerAnimator;

    void Awake()
    {
        Timer = new WaitForSecondsRealtime(0.025f);
    }

    public void Open()
    {
        Canvas.SetActive(true);
    }

    public void Close()
    {
        Canvas.SetActive(false);
    }

    public void Display(string speaker, string text, List<Choice> choices, Action<Choice> onChoice, Animator speakerAnimator)
    {
        Clear();
        SpeakerAnimator = speakerAnimator;
        DarkOverlay.SetActive(false);

        Speaker.text = speaker;
        LineToWrite = text;

        Choices = choices;
        OnChoice = onChoice;

        TypeWritingCoroutine = TypeWriteLine();
        if (speakerAnimator != null)
        {
            speakerAnimator.SetTrigger("Talk");
        }
        StartCoroutine(TypeWritingCoroutine);
    }

    public void ClickLine()
    {
        if (IsWriting)
        {
            SkipTypeWriting();
        }
        else
        {
            if (Choices.Count > 0)
            {
                WriteChoices(Choices, OnChoice);
            }
            else
            {
                OnChoice(null);
            }
        }
    }

    public void SkipTypeWriting()
    {
        StopCoroutine(TypeWritingCoroutine);
        IsWriting = false;
        Line.maxVisibleCharacters = LineToWrite.Length;
        Line.text = LineToWrite;
        if (SpeakerAnimator != null)
        {
            SpeakerAnimator.SetTrigger("DoneTalk");
        }

    }

    public void WriteChoices(List<Choice> choices, Action<Choice> onChoice)
    {
        Clear();

        DarkOverlay.SetActive(true);

        if (choices.Count > 0)
        {
            for (int i = 0; i < choices.Count; i++)
            {
                Choice choice = choices[i];

                GameObject go = Instantiate(ResponsePrefab, ResponseSpawn);

                go.GetComponentInChildren<TMP_Text>().text = choice.Text;
                go.GetComponentInChildren<Button>().onClick.AddListener(() => onChoice(choice));
                go.transform.SetParent(ChoicesContainer.transform);

                Responses.Add(go);
            }
        }
    }

    private void Clear()
    {
        foreach (GameObject response in Responses)
        {
            Destroy(response);
        }
    }

    private IEnumerator TypeWriteLine()
    {
        IsWriting = true;

        Line.maxVisibleCharacters = 0;
        Line.text = LineToWrite;

        for (int i = 0; i < LineToWrite.Length; i++)
        {
            Line.maxVisibleCharacters = i + 1;
            yield return Timer;
        }
        if (SpeakerAnimator != null)
        {
            SpeakerAnimator.SetTrigger("DoneTalk");
        }
        IsWriting = false;
    }
}
