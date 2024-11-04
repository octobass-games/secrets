using System;
using System.Collections.Generic;

[Serializable]
public class Interaction
{
    public int RelationshipRequirement;
    public List<Dialogue> Dialogues;

    public Interaction(int relationshipRequirement, List<Dialogue> dialogues)
    {
        RelationshipRequirement = relationshipRequirement;
        Dialogues = dialogues;
    }
}
