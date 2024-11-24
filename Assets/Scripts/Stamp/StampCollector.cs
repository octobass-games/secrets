using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class StampCollector : MonoBehaviour, Savable
{
    public List<StampDefinition> Stamps;
    public GameObject StampBook;
    public List<Stamp> BookStamps;

    public List<GameObject> Pages;
    private int PageIndex;

    void Awake()
    {
        Stamps = Stamps.Select(stamp => Instantiate(stamp)).ToList();
    }

    void OnEnable()
    {
        EventManager.Instance.Subscribe(GameEventType.STAMP_COLLECTED, OnStampCollected);
    }

    void OnDisable()
    {
        EventManager.Instance.Unsubscribe(GameEventType.STAMP_COLLECTED, OnStampCollected);
    }

    public void ShowStamps()
    {
        StampBook.SetActive(true);
        UpdatePagesShown();

        foreach (var stamp in Stamps)
        {
            var bookStamp = BookStamps.Find(s => stamp.Name == s.StampDefinition.Name);
            
            if (stamp.IsUnlocked)
            {
                bookStamp.ShowUnlocked();
            }
            else
            {
                bookStamp.ShowLocked();
            }
        }
    }

    public void HideStamps()
    {
        StampBook.SetActive(false);
        PageIndex = 0;
    }

    private void OnStampCollected(GameEvent @event)
    {
        StampDefinition stamp;

        if (@event.Stamp != null)
        {
            stamp = Stamps.Find(s => s.IsEqual(@event.Stamp));
        }
        else
        {
            stamp = Stamps.Find(s => s.Name == @event.StampId);
        }

        stamp.IsUnlocked = true;
    }

    public void Load(SaveData saveData)
    {
        foreach (var stamp in saveData.Stamps)
        {
            var stampDefinition = Stamps.Find(s => s.Name == stamp.Id);

            if (stampDefinition != null)
            {
                stampDefinition.IsUnlocked = stamp.IsUnlocked;
            }
        }
    }

    public void Save(SaveData saveData)
    {
        saveData.Stamps = Stamps.Select(s => new StampData(s.Name, s.IsUnlocked)).ToList();
    }

    public void NextPage()
    {
        PageIndex = Pages.NextIndex(PageIndex);

        UpdatePagesShown();
    }

    public void PrevPage()
    {
        PageIndex = Pages.PrevIndex(PageIndex);

        UpdatePagesShown();
    }

    private void UpdatePagesShown()
    {
        for (int i = 0; i < Pages.Count; i++)
        {
            var page = Pages[i];
            page.SetActive(i == PageIndex);
        }
    }
}
