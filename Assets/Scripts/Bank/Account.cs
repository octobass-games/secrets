using UnityEngine;
using UnityEngine.Events;

public class Account : MonoBehaviour
{
    [SerializeField]
    private int Balance;

    public UnityEvent<int> OnOpen;
    public UnityEvent<int> OnDeposit;
    public UnityEvent<int> OnWithdraw;
    public UnityEvent<int> OnBankrupt;

    void Start()
    {
        OnOpen?.Invoke(Balance);    
    }

    public void Deposit(int amount)
    {
        Balance += amount;

        OnDeposit?.Invoke(Balance);
    }

    public void Withdraw(int amount)
    {
        Balance -= amount;

        if (Balance <= 0)
        {
            OnBankrupt?.Invoke(Balance);
        }
        else
        {
            OnWithdraw?.Invoke(Balance);
        }
    }
}
