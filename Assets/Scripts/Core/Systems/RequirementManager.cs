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
            case RequirementType.HISTORY:
                satisfied = History.Contains(requirement.Name);
                break;
            case RequirementType.INVENTORY_EMPTY:
                satisfied = Inventory.IsEmpty();
                break;
            case RequirementType.INVENTORY_CONTAINS:
                satisfied = Inventory.Contains(requirement.Name);
                break;
            case RequirementType.INVENTORY_NOT_EMPTY:
                satisfied = Inventory.IsNotEmpty();
                break;
            default:
                break;
        }

        return requirement.Negate ? !satisfied : satisfied;
    }
}
