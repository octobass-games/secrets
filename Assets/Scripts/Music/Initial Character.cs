using UnityEngine;

public class InitialCharacter : MonoBehaviour
{

    private FMOD.Studio.EventInstance instance;
    public FMODUnity.EventReference fmodEvent;

    public string parameter;
    public string parameterValue;

    void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        instance.setParameterByNameWithLabel(parameter, parameterValue);
       
    }
}
