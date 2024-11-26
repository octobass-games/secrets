using UnityEngine;
using UnityEngine.UI;

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
            }catch
            {

            }
        }
    }
}
