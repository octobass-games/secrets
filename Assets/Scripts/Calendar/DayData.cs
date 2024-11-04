using System;

[Serializable]
public class DayData
{
    public string Date;
    public bool IsInPast;

    public DayData(string date, bool isInPast)
    {
        Date = date;
        IsInPast = isInPast;
    }
}
