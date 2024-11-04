using System;
using System.Collections.Generic;

[Serializable]
public class CalendarData
{
    public List<DayData> Days;

    public CalendarData(List<DayData> days)
    {
        Days = days;
    }
}
