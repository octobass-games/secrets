using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    public string Track;

    private FMOD.Studio.EventInstance MusicInstance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
    }

    void Start()
    {
        CreateAndPlayInstance();
    }

    void OnDestroy()
    {
        if (MusicInstance.isValid())
        {
            MusicInstance.release();
        }
    }

    public void ChangeTrack(string track)
    {
        if (MusicInstance.isValid())
        {
            MusicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            MusicInstance.release();
            MusicInstance.clearHandle();
        }

        Track = track;

        CreateAndPlayInstance();
    }

    private void CreateAndPlayInstance()
    {
        if (Track != null && Track != "")
        { 
            MusicInstance = FMODUnity.RuntimeManager.CreateInstance(Track);
            MusicInstance.start();
        }
    }
}
