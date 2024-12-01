using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RequirementManager : MonoBehaviour
{
    public History History;
    public Bookkeeper Bookkeeper;

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
                satisfied = Bookkeeper.NoBookAtTill();
                break;
            case RequirementType.INVENTORY_CONTAINS:
                if (requirement.Book != null)
                {
                    satisfied = Bookkeeper.IsBookAtTill(requirement.Book);
                }
                else if (requirement.Books != null && requirement.Books.Count > 0)
                {
                    satisfied = requirement.Books.Find(b => Bookkeeper.IsBookAtTill(b)) != null;
                }
                break;
            case RequirementType.INVENTORY_NOT_EMPTY:
                satisfied = !Bookkeeper.NoBookAtTill();
                break;
            case RequirementType.BANK_BALANCE_AVAILABLE:
                satisfied = Bookkeeper.IsAffordablePayment(requirement.Amount);
                break;
            case RequirementType.BOOK_WITH_ITEM:
                satisfied = Bookkeeper.IsBookAtTillWithItem(requirement.Item);
                break;
            case RequirementType.BOOK_WITH_SOME_ITEM:
                satisfied = Bookkeeper.IsBookAtTillWithSomeItem();
                break;
            case RequirementType.CAN_AFFORD_TAXES:
                satisfied = Bookkeeper.CanAffordTaxes();
                break;
            case RequirementType.CAN_AFFORD_RENT:
                satisfied = Bookkeeper.CanAffordRent();
                break;
            default:
                break;
        }

        return requirement.Negate ? !satisfied : satisfied;
    }
}
