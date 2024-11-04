using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Calendar : MonoBehaviour, Savable
{
    public List<DayDefinition> DayDefinitions = new();

    private DayDefinition Today;

    void Start()
    {
        Today = FindNextDay();
    }

    public void StartDay()
    {

    }

    public void EndDay()
    {
        Today.IsInThePast = true;

        Today = FindNextDay();
    }

    public void Load(SaveData saveData)
    {
        List<DayData> Days = saveData.Calendar.Days;

        foreach (DayData day in Days)
        {
            DayDefinition dayDefinition = DayDefinitions.Find(d => d.Date == day.Date);

            dayDefinition.IsInThePast = day.IsInPast;
        }
    }

    public void Save(SaveData saveData)
    {
        List<DayData> days = DayDefinitions.Select(d => new DayData(d.Date, d.IsInThePast)).ToList();

        saveData.Calendar = new CalendarData(days);
    }

    private DayDefinition FindNextDay()
    {
        return DayDefinitions.Find(d => !d.IsInThePast);
    }
}
