using TMPro;
using UnityEngine;

public class AccountView : MonoBehaviour
{
    public TMP_Text Balance;

    public void Display(int amount)
    {
        Balance.text = amount.ToString();    
    }
}
