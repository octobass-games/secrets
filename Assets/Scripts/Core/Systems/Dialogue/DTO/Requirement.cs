using System;

[Serializable]
public class Requirement
{
    public RequirementType Type;
    public string Name;
    public BookDefinition Book;
    public bool Negate;
}
