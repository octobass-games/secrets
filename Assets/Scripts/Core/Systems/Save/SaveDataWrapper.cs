using System;
using System.Collections.Generic;

[Serializable]
public class SaveDataWrapper
{
    public List<SaveData> TimePoints;

    public SaveDataWrapper(List<SaveData> timePoints)
    {
        TimePoints = timePoints;
    }

    public SaveDataWrapper()
    {
        TimePoints = new();
    }
}
