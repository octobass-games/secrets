using UnityEngine;
using UnityEngine.Events;

public class Till : MonoBehaviour, Savable
{
    public UnityEvent<int> OnDeposit;
    public UnityEvent<int> OnWithdraw;
    public UnityEvent<int> OnBankrupt;

    [SerializeField]
    private int Balance;

    public void SellBook(Book book)
    {
        if (book.InStock())
        {
            Balance += book.GetSellPrice();

            OnDeposit?.Invoke(Balance);
        }
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

    public void Save(SaveData saveData)
    {
        saveData.Account = new TillData(Balance);
    }

    public void Load(SaveData saveData)
    {
        Balance = saveData.Account.Balance;
    }
}
