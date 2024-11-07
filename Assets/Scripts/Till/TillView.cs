using TMPro;
using UnityEngine;

public class TillView : MonoBehaviour
{
    public TMP_Text Balance;

    public void Display(int amount)
    {
        Balance.text = amount.ToString();    
    }
}
