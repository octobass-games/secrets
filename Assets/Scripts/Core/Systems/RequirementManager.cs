using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RequirementManager : MonoBehaviour
{
    public History History;
    public Inventory Inventory;

    public bool AllSatisfied(List<Requirement> requirements)
    {
        return requirements.All(IsSatisfied);
    }

    private bool IsSatisfied(Requirement requirement)
    {
        bool satisfied = false;

        switch (requirement.Type)
        {
            case "history":
                satisfied = History.Contains(requirement.Name);
                break;
            case "inventory.empty":
                satisfied = Inventory.IsEmpty();
                break;
            case "inventory.contains":
                satisfied = Inventory.Contains(requirement.Name);
                break;
            case "inventory.not.empty":
                satisfied = Inventory.IsNotEmpty();
                break;
            default:
                break;
        }

        return requirement.Negate ? !satisfied : satisfied;
    }
}
