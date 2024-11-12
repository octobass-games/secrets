using UnityEngine;
using UnityEngine.UI;

public class PaperDisplayer : MonoBehaviour
{
    public GameObject IssueOne;
    public Button IssueOneClose;
    public GameObject IssueTwo;
    public GameObject IssueThree;
    public GameObject IssueFour;

    public PaperDefinition PaperIssueOne;
    public PaperDefinition PaperIssueTwo;
    public PaperDefinition PaperIssueThree;
    public PaperDefinition PaperIssueFour;

    public DayManager DayManager;

    public void RenderPaperFirstTime(PaperDefinition paper)
    {
        if (paper == PaperIssueOne)
        {
            IssueOne.SetActive(true);

            IssueOneClose.onClick.RemoveAllListeners();
            IssueOneClose.onClick.AddListener(() =>
            {
                DayManager.NextEvent();
                IssueOne.SetActive(false);

                IssueOneClose.onClick.RemoveAllListeners();
                IssueOneClose.onClick.AddListener(() => IssueOne.SetActive(false));
            });
        }
        if (paper == PaperIssueTwo)
        {
            IssueOne.SetActive(true);
        }
        if (paper == PaperIssueThree)
        {
            IssueOne.SetActive(true);
        }
        if (paper == PaperIssueFour)
        {
            IssueOne.SetActive(true);
        }
    }
}
