using System;

[Serializable]
public class Requirement
{
    public string Type;
    public string Name;
    public bool Negate;

    public Requirement(string type, string name, bool negate)
    {
        Type = type;
        Name = name;
        Negate = negate;
    }
}
