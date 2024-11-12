using UnityEngine;

public class PaperDisplayer : MonoBehaviour
{
    public GameObject IssueOne;
    public GameObject IssueTwo;
    public GameObject IssueThree;
    public GameObject IssueFour;

    public PaperDefinition PaperIssueOne;
    public PaperDefinition PaperIssueTwo;
    public PaperDefinition PaperIssueThree;
    public PaperDefinition PaperIssueFour;

    public void RenderPaper(PaperDefinition paper)
    {
        if (paper == PaperIssueOne)
        {
            IssueOne.SetActive(true);
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
