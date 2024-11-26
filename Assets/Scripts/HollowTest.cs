using UnityEngine;

public class HollowTest : MonoBehaviour
{
    public Bookkeeper Bookkeeper;
    public BookDefinition BookDefinition;
    public ItemDefinition ItemDefinition;

    private void OnMouseDown()
    {
        Debug.Log("Hollowing");

        var b = Bookkeeper.HollowBook(BookDefinition);
        Bookkeeper.InsertIntoHollowBook(b, ItemDefinition);
    }
}
