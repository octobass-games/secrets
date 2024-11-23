using System.Collections.Generic;
using UnityEngine;

public class PaperStacker : MonoBehaviour, Savable
{
    public GameObject PostOpen;
    public GameObject PostClosed;
    public List<GameObject> Papers;
    public PaperDisplayer PaperDisplayer;

    private int PaperCount = 0;

    void OnEnable()
    {
        EventManager.Instance.Subscribe(GameEventType.PAPER_ARRIVED, OnPaperArrived);
    }

    void OnDisable()
    {
        EventManager.Instance.Unsubscribe(GameEventType.PAPER_ARRIVED, OnPaperArrived);
    }

    public void OnPaperArrived(GameEvent @event)
    {
        PostOpen.SetActive(true);
        PostClosed.SetActive(true);
        var OnClickPaper = PostOpen.GetComponent<Clickable>().OnClick;
        OnClickPaper.RemoveAllListeners();
        OnClickPaper.AddListener(() =>
        {
            PaperDisplayer.RenderPaperFirstTime(@event.Paper);
            PostOpen.SetActive(false);
            PostClosed.SetActive(true);
            PutPaperAway();
        });
    }

    public void PutPaperAway()
    {
        PaperCount++;
        RenderPapers();
    }

    private void RenderPapers()
    {
        for (int i = 0; i < PaperCount; i++)
        {
            Papers[i].SetActive(true);
        }
    }

    public void Save(SaveData saveData)
    {
        saveData.PaperCount = PaperCount;
    }

    public void Load(SaveData saveData)
    {
        PaperCount = saveData.PaperCount;

        RenderPapers();
    }
}
