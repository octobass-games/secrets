using System.Collections.Generic;
using UnityEngine;

public class PaperStacker : MonoBehaviour, EventSubscriber, Savable
{
    public GameObject PostOpen;
    public GameObject PostClosed;
    public List<GameObject> Papers;
    public PaperDisplayer PaperDisplayer;

    private int PaperCount = 0;

    void Awake()
    {
        EventManager.Instance.Subscribe(GameEventType.PAPER_ARRIVED, this);
    }

    public void OnReceive(GameEvent @event)
    {
        PostOpen.SetActive(true);
        PostClosed.SetActive(true);
        var OnClickPaper = PostOpen.GetComponent<Clickable>().OnClick;
        OnClickPaper.RemoveAllListeners();
        OnClickPaper.AddListener(() =>
        {
            PaperDisplayer.RenderPaper(@event.Paper);
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
