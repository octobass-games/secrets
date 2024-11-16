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

    public void DisplayImmediately(int amount)
    {
        TryStopChangingBalance();

        Balance = amount;

        VisibleBalance.text = Balance.ToString();
    }

    public void Display(int amount)
    {
        TryStopChangingBalance();

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

    private void TryStopChangingBalance()
    {
        if (ChangeVisibleBalanceCoroutine != null)
        {
            StopCoroutine(ChangeVisibleBalanceCoroutine);

            ChangeVisibleBalanceCoroutine = null;
        }
    }
}
