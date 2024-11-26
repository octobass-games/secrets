using TMPro;
using UnityEngine;

public class BookInspector : MonoBehaviour
{
    public GameObject InspectorView;
    public GameObject Knife;

    public TMP_Text BookTitleView;

    public void ShowInspector(BookDefinition definition)
    {
        InspectorView.SetActive(true);

        BookTitleView.text = definition.Name;
    }

    public void HideInspector()
    {
        InspectorView.SetActive(false);
    }
}
