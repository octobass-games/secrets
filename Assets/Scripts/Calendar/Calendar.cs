using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Calendar : MonoBehaviour, Savable
{
    public List<DayDefinition> DayDefinitions = new();
    public TMP_Text CalendarText;

    private DayDefinition Today;

    void Start()
    {
        Today = FindNextDay();
    }

    public void StartDay()
    {
        CalendarText.text = Today.Date;
    }

    public void EndDay()
    {
        Today.IsInThePast = true;

        Today = FindNextDay();
    }

    public void Load(SaveData saveData)
    {
        foreach (DayData day in saveData.Days)
        {
            DayDefinition dayDefinition = DayDefinitions.Find(d => d.Date == day.Date);

            dayDefinition.IsInThePast = day.IsInPast;
        }
    }

    public void Save(SaveData saveData)
    {
        List<DayData> days = DayDefinitions.Select(d => new DayData(d.Date, d.IsInThePast)).ToList();

        saveData.Days = days;
    }

    private DayDefinition FindNextDay()
    {
        return DayDefinitions.Find(d => !d.IsInThePast);
    }
}
