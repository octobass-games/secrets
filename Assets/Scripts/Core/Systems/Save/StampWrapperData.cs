using System;
using System.Collections.Generic;

[Serializable]
public class StampWrapperData
{
    public List<StampData> StampData;

    public StampWrapperData(List<StampData> stampData)
    {
        StampData = stampData;
    }

    public StampWrapperData() { }
}
