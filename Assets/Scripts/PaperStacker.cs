using UnityEngine;
using UnityEngine.Events;

public class PaperStacker :  MonoBehaviour, EventSubscriber
{
    public Calendar Calendar;

    public int PaperCount = 0;

    public GameObject PaperOne;
    public GameObject PaperTwo;
    public GameObject PaperThree;
    public GameObject PaperFour;

    public GameObject PostOpen;
    public GameObject PostClosed;


    void Awake()
    {
        EventManager.Instance.Subscribe(GameEventType.PAPER_ARRIVED, this);
    }

    public void OnReceive(GameEvent @event)
    {
        PostOpen.SetActive(true);
        PostClosed.SetActive(true);
    }

    public void PutPaperAway()
    {
        PaperCount++;
        RenderPapers();
    }

    private void RenderPapers()
    {
        PaperOne.SetActive(false);
        PaperTwo.SetActive(false);
        PaperThree.SetActive(false);
        PaperFour.SetActive(false);
        if (PaperCount >= 1)
        {
            PaperOne.SetActive(true);
        }

        if (PaperCount >= 2)
        {
            PaperTwo.SetActive(true);
        }

        if (PaperCount >= 3)
        {
            PaperThree.SetActive(true);
        }

        if (PaperCount >= 4)
        {
            PaperFour.SetActive(true);
        }
    }
}
