using System.Collections.Generic;
using System.Linq;
using UnityEngine;



//Looking Sharp, Weapons Maintenance 101   0053
//The Lost Wolf 0010
//The Castle on the hill 0022
//Botany and you 0004
//Lost or am I 0030

//119

public class Safe : MonoBehaviour
{
    private List<int> ExpectedOrder = new List<int>() { 1,1,9};
    private List<int> AttemptedOrder = new List<int>() { };
    public GameObject OpenSafe;
    public GameObject ClosedSafe;
    public GameObject Painting;
    public StampDefinition UnlockSafeStamp;
    public void SafeReset()
    {
        AttemptedOrder = new List<int>();    

    }

    public void PressButton(int button)
    {
        AttemptedOrder.Add(button);
    }

    public void AttemptOpen()
    {
        var last3Entries = AttemptedOrder.TakeLast(3);

        if (last3Entries.SequenceEqual(ExpectedOrder))
        {
            ClosedSafe.SetActive(false);
            Painting.SetActive(false);
            OpenSafe.SetActive(true);
            EventManager.Instance.Publish(new GameEvent() { Type = GameEventType.STAMP_COLLECTED, Stamp = UnlockSafeStamp });
        }
        else
        {
            SafeReset();
        }
    }

}
