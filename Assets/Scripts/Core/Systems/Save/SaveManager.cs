using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private string SaveFilePath;
    private string StampFilePath;

    void Awake()
    {
        SaveFilePath = Application.persistentDataPath + "/save-data.json";
        StampFilePath = Application.persistentDataPath + "/save-stamp.json";

        Load();
    }

    public void Save()
    {
        SaveData saveData = new();

        List<Savable> savables = FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None).OfType<Savable>().ToList();
        
        foreach (Savable savable in savables)
        {
            savable.Save(saveData);
        }

        var wrapper = ReadSaveData();

        wrapper.TimePoints.Add(saveData);

        WriteData(wrapper, SaveFilePath, "save-data");
        
        StampCollector stampCollector = FindFirstObjectByType<StampCollector>();

        if (stampCollector != null)
        {
            var stamps = stampCollector.GetSaveData();

            var data = new StampWrapperData(stamps);

            WriteData(data, StampFilePath, "save-stamp");
        }
    }

    public void Load()
    {
        if (HasData(SaveFilePath, "save-data"))
        {
            var wrapper = ReadSaveData();
            
            List<Savable> savables = FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None).OfType<Savable>().ToList();

            foreach (Savable savable in savables)
            {
                if (wrapper.TimePoints.Count > 0)
                {
                    savable.Load(wrapper.TimePoints[wrapper.TimePoints.Count-1]);
                }
            }
        }

        if (HasData(StampFilePath, "save-stamp"))
        {
            StampCollector stampCollector = FindFirstObjectByType<StampCollector>();

            if (stampCollector != null)
            {
                var stamps = ReadStampData();
                
                stampCollector.Load(stamps.StampData);
            }
        }
    }

    public void Rewind(int dayIndex)
    {
        var wrapper = ReadSaveData();

        wrapper.TimePoints = wrapper.TimePoints.Take(dayIndex).ToList();

        WriteSaveData(wrapper);

        UnityEngine.SceneManagement.SceneManager.LoadScene("Shop");
    }

    public void RestartDay()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Shop");
    }

    public void DeleteSaveData()
    {
        if (HasData(SaveFilePath, "save-data"))
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
        return HasData(SaveFilePath, "save-data");
    }

    public bool HasStampData()
    {
        return HasData(StampFilePath, "save-stamp");
    }

    private SaveDataWrapper ReadSaveData()
    {
        return ReadData<SaveDataWrapper>(SaveFilePath, "save-data");
    }

    private StampWrapperData ReadStampData()
    {
        return ReadData<StampWrapperData>(StampFilePath, "save-stamp");
    }

    private void WriteSaveData(SaveDataWrapper wrapper)
    {
        WriteData(wrapper, SaveFilePath, "save-data");
    }

    private void WriteStampData(StampData wrapper)
    {
        WriteData(wrapper, StampFilePath, "save-stamp");
    }

    private bool HasData(string path, string key)
    {
        if (Application.platform != RuntimePlatform.WebGLPlayer)
        {
            return File.Exists(path);
        }
        else
        {
            return PlayerPrefs.GetString(key) != "";
        }
    }

    private T ReadData<T>(string path, string key) where T : new()
    {
        if (HasData(path, key))
        {
            string json;

            if (Application.platform != RuntimePlatform.WebGLPlayer)
            {
                using var streamReader = new StreamReader(path);
                json = streamReader.ReadToEnd();
            }
            else
            {
                json = PlayerPrefs.GetString(key);
            }

            return JsonUtility.FromJson<T>(json);
        }

        return new T();
    }

    private void WriteData<T>(T wrapper, string path, string key)
    {
        var json = JsonUtility.ToJson(wrapper);

        if (Application.platform != RuntimePlatform.WebGLPlayer)
        {
            using var fileStream = new FileStream(path, FileMode.Create);
            using var streamWriter = new StreamWriter(fileStream);

            streamWriter.Write(json);
        }
        else
        {
            PlayerPrefs.SetString(key, json);
            PlayerPrefs.Save();
        }
    }
}
