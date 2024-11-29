using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        List<Savable> savables = FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None).OfType<Savable>().ToList();

        foreach (Savable savable in savables)
        {
            savable.Save(saveData);
        }

        var wrapper = ReadSaveData();

        wrapper.TimePoints.Add(saveData);

        WriteSaveData(wrapper);
    }

    public void Load()
    {
        if (HasSaveData())
        {
            var wrapper = ReadSaveData();
            
            List<Savable> savables = FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None).OfType<Savable>().ToList();

            foreach (Savable savable in savables)
            {
                savable.Load(wrapper.TimePoints[wrapper.TimePoints.Count-1]);
            }
        }
    }

    public void Rewind(int dayIndex)
    {
        if (dayIndex == 0)
        {
            DeleteSaveData();
        }
        else
        {
            var wrapper = ReadSaveData();

            wrapper.TimePoints = wrapper.TimePoints.Take(dayIndex).ToList();

            WriteSaveData(wrapper);
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene("Shop");
    }

    public void RestartDay()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Shop");
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

    private void WriteSaveData(SaveDataWrapper wrapper)
    {
        var json = JsonUtility.ToJson(wrapper);

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

    private SaveDataWrapper ReadSaveData()
    {
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

            return JsonUtility.FromJson<SaveDataWrapper>(json);
        }

        return new SaveDataWrapper();
    }
}
