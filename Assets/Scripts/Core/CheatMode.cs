using UnityEngine;

public class CheatMode : MonoBehaviour
{
    public DayManager DayManager;
    public TMPro.TMP_InputField CheatInput;
    void Awake()
    {
       
    }

    void Update()
    {
    }

    public void SubmitCheat()
    {
        Debug.Log("Submit Cheat: " + CheatInput.text);
        if (CheatInput.text.StartsWith("/skip-to-day ") || CheatInput.text.StartsWith("day "))
        {
            var dayString = CheatInput.text.Replace("/skip-to-day ", "").Replace("day ", "");
            try
            {
                var day = int.Parse(dayString);
                DayManager.SkipToDay(day);
            }
            catch
            {

            }
        }
        else if (CheatInput.text.Equals("motherlode"))
        {
            EventManager.Instance.Publish(new GameEvent() { Type = GameEventType.BANK_DEPOSIT, Amount = 50000 });
        }
        else if (CheatInput.text.Equals("/kill"))
        {
            EventManager.Instance.Publish(new GameEvent() { Type = GameEventType.GAME_OVER, Day = DayManager.GetToday(), Message = "You cheated"  });
        }
    }
}
