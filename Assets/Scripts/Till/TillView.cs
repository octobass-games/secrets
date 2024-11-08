using System.Collections;
using TMPro;
using UnityEngine;

public class TillView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text VisibleBalance;

    private int Balance;

    private IEnumerator ChangeVisibleBalanceCoroutine;
    private WaitForSecondsRealtime WaitBetweenChanges = new(0.0050f);

    public void Display(int amount)
    {
        if (ChangeVisibleBalanceCoroutine != null)
        {
            StopCoroutine(ChangeVisibleBalanceCoroutine);
        }

        ChangeVisibleBalanceCoroutine = ChangeVisibleBalance(amount);

        StartCoroutine(ChangeVisibleBalanceCoroutine);
    }

    private IEnumerator ChangeVisibleBalance(int amount)
    {
        while (Balance != amount)
        {
            if (Balance < amount)
            {
                Balance++;
            }
            else if (Balance > amount)
            {
                Balance--;
            }

            VisibleBalance.text = Balance.ToString();

            yield return WaitBetweenChanges;
        }

        ChangeVisibleBalanceCoroutine = null;
    }
}
