using UnityEngine;

public class UpcomingPayments : MonoBehaviour
{
    public Bookkeeper Bookkeeper;
    public DialogueManager DialogueManager;

    public Line MonthlyRent8000;
    public Line MonthlyRent7900;
    public Line MonthlyRent7850;
    public Line MonthlyRentOriginal;

    public void ReadUpcomingPayment()
    {
        int monthlyRent = Bookkeeper.GetMonthlyRent();

        if (monthlyRent == 8000)
        {
            DialogueManager.Begin(MonthlyRent8000);
        }
        else if (monthlyRent == 7900)
        {
            DialogueManager.Begin(MonthlyRent7900);
        }
        else if (monthlyRent == 7850)
        {
            DialogueManager.Begin(MonthlyRent7850);
        }
        else
        {
            DialogueManager.Begin(MonthlyRentOriginal);
        }
    }
}
