using UnityEngine;

public class InitialCharacter : MonoBehaviour
{

    private FMOD.Studio.EventInstance instance;
    public FMODUnity.EventReference fmodEvent;

    [SerializeField] private float parameter =  Random.Range(0,6);

    void Start()
    {

        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        instance.setParameterByName("Character", parameter);

        Debug.Log(parameter);
    }
}
