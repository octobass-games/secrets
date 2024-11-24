using System;
using System.Collections.Generic;

[Serializable]
public class Requirement
{
    public RequirementType Type;
    public string Name;
    public BookDefinition Book;
    public List<BookDefinition> Books;
    public bool Negate;
    public int Amount;
}
