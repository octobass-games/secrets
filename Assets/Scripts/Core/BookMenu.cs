using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BookMenu : MonoBehaviour
{
    public GameObject Book;
    public Animator BookAnimator;
    public List<GameObject> Pages;
    private int PageIndex;

    public void ShowPagees()
    {
        Book.SetActive(true);
        UpdatePagesShown();
    }

    public void HidePages()
    {
        Book.SetActive(false);
        PageIndex = 0;
    }

    public void NextPage()
    {
        PageIndex = Pages.NextIndex(PageIndex);
        BookAnimator.SetTrigger("turnforwards");

        UpdatePagesShown();
    }

    public void PrevPage()
    {
        PageIndex = Pages.PrevIndex(PageIndex);
        BookAnimator.SetTrigger("turnback");

        UpdatePagesShown();
    }

    public void TurnToPage(int index)
    {
        if (index > PageIndex)
        {
            BookAnimator.SetTrigger("turnforwards");
        }
        else if (index < PageIndex)
        {
            BookAnimator.SetTrigger("turnback");
        }
        PageIndex = index;

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
