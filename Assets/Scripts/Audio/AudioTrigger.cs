using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    private FMOD.Studio.EventInstance instance;
    public string fmodEvent;
    public bool is3D;

    private void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);

        if (is3D == true)
        {
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(instance, GetComponent<Transform>(), GetComponent<BoxCollider2D>());
        }
    }

    public void FmodOneShot()
    {
        FMODUnity.RuntimeManager.PlayOneShot(fmodEvent);
    }

    public void PlayFmodEvent()
    {
        instance.start();
    }

    public void StopFmodEvent()
    {
        instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    void OnDestroy()
    {
        instance.release();    
    }
}
