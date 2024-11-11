using System;
using System.Collections.Generic;

[Serializable]
public class BookkeeperData
{
    public int BankBalance;
    public List<SalesRecord> SalesRecords;

    public BookkeeperData(int bankBalance, List<SalesRecord> salesRecords)
    {
        BankBalance = bankBalance;
        SalesRecords = salesRecords;
    }
}
