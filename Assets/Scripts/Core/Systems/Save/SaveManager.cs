using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance {  get; private set; }

    private string SaveFilePath;

    void Awake()
    {
        SaveFilePath = Application.persistentDataPath + "/save-data.json";

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }

        Load();
    }

    public void Save()
    {
        SaveData saveData = new();

        List<Savable> savables = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<Savable>().ToList();

        foreach (Savable savable in savables)
        {
            savable.Save(saveData);
        }

        var json = JsonUtility.ToJson(saveData);

        if (Application.platform != RuntimePlatform.WebGLPlayer)
        {
            using var fileStream = new FileStream(SaveFilePath, FileMode.Create);
            using var streamWriter = new StreamWriter(fileStream);

            streamWriter.Write(json);
        }
        else
        {
            PlayerPrefs.SetString("save-data", json);
            PlayerPrefs.Save();
        }
    }

    public void Load()
    {
        SaveData saveData = new();

        if (HasSaveData())
        {
            string json;

            if (Application.platform != RuntimePlatform.WebGLPlayer)
            {
                using var streamReader = new StreamReader(SaveFilePath);
                json = streamReader.ReadToEnd();
            }
            else
            {
                json = PlayerPrefs.GetString("save-data");
            }

            saveData = JsonUtility.FromJson<SaveData>(json);
        }

        List<Savable> savables = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<Savable>().ToList();

        foreach (Savable savable in savables)
        {
            savable.Load(saveData);
        }
    }

    public void DeleteSaveData()
    {
        if (HasSaveData())
        {
            if (Application.platform != RuntimePlatform.WebGLPlayer)
            {
                File.Delete(SaveFilePath);
            }
            else
            {
                PlayerPrefs.DeleteKey("save-data");
            }
        }
    }

    public bool HasSaveData()
    {
        if (Application.platform != RuntimePlatform.WebGLPlayer)
        {
            return File.Exists(SaveFilePath);
        }
        else
        {
            return PlayerPrefs.GetString("save-data") != "";
        }
    }
}
