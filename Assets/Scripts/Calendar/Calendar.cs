using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Calendar : MonoBehaviour, Savable
{
    public List<DayDefinition> DayDefinitions = new();
    public List<Character> Characters;
    public TMP_Text CalendarText;

    private DayDefinition Today;
    public List<Character> CharactersVisitingToday = new();

    void Start()
    {
        Today = FindNextDay();
        StartDay();
    }

    public void StartDay()
    {
        CalendarText.text = Today.Date;

        foreach (Character character in Characters)
        {
            if (Today.VisitingCharacters.Find(c => c.Name == character.CharacterDefinition.Name) == null)
            {
                character.gameObject.SetActive(false);
            }
            else
            {
                CharactersVisitingToday.Add(character);

                if (CharactersVisitingToday.Count == 1)
                {
                    character.gameObject.SetActive(true);
                }
                else
                {
                    character.gameObject.SetActive(false);
                }
            }
        }
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
