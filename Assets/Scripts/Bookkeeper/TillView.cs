using System.Collections;
using TMPro;
using UnityEngine;

public class TillView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text VisibleBalance;

    private int Balance;

    private IEnumerator ChangeVisibleBalanceCoroutine;
    private int TimeForChangeInSeconds = 2;
    private float TimeBetweenChangesInSeconds = 0.05f;
    private int NumberOfChangesToReachTargetValue;
    private WaitForSecondsRealtime WaitBetweenChanges;

    void Awake()
    {
        WaitBetweenChanges = new(TimeBetweenChangesInSeconds);
        NumberOfChangesToReachTargetValue = Mathf.CeilToInt(TimeForChangeInSeconds / TimeBetweenChangesInSeconds);
    }

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
        var differential = Mathf.Abs(Balance - amount) / NumberOfChangesToReachTargetValue;

        while (Balance != amount)
        {
            if (Balance < amount)
            {
                Balance = Mathf.Min(Balance + differential, amount);
            }
            else if (Balance > amount)
            {
                Balance = Mathf.Max(Balance - differential, amount);
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
