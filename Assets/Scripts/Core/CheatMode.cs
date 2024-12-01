using System.Linq;
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
        if (CheatInput.text.Equals("motherlode"))
        {
            EventManager.Instance.Publish(new GameEvent() { Type = GameEventType.BANK_DEPOSIT, Amount = 50000 });
        }
        else if (CheatInput.text.Equals("/kill"))
        {
            EventManager.Instance.Publish(new GameEvent() { Type = GameEventType.GAME_OVER, Day = DayManager.GetToday(), Message = "You cheated" });
        }
        else if (CheatInput.text.Equals("bankrupt"))
        {
            EventManager.Instance.Publish(new GameEvent() { Type = GameEventType.BANK_WITHDRAWAL, Amount = 50000 });
        }
        else if (CheatInput.text.Equals("tidbits"))
        {
            var characters = FindObjectsByType<Character>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();
            characters.ForEach(c =>
            {
                c.CharacterDefinition.Tidbits.ForEach(t => c.UnlockTidbit(t));
            });
        }
    }
}
