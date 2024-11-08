using UnityEngine;
using UnityEngine.Events;

public class Till : MonoBehaviour, Savable, EventSubscriber
{
    public Inventory Inventory;

    public UnityEvent<int> OnDeposit;
    public UnityEvent<int> OnWithdraw;
    public UnityEvent<int> OnBankrupt;

    [SerializeField]
    private int Balance;

    void Start()
    {
        FindFirstObjectByType<EventManager>().Subscribe(GameEventType.INVENTORY_SELL, this);
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

    public void OnReceive(GameEvent @event)
    {
        // Todo: Handle decrementing book stock etc.
        Balance += Inventory.GetBook().GetSellPrice();
        OnDeposit?.Invoke(Balance);
    }
}
