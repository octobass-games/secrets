using System;

[Serializable]
public class AccountData
{
    public int Balance;

    public AccountData(int balance)
    {
        Balance = balance;
    }
}
