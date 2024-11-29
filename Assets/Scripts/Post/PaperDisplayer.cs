using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaperDisplayer : MonoBehaviour
{

    public List<PaperDefinition> PaperDefinitions;
    public List<GameObject> Issues;
    public List<Button> CloseButtons;

    public DayManager DayManager;

    public void RenderPaperFirstTime(PaperDefinition paper)
    {

        int index = PaperDefinitions.FindIndex(p => p.Name == paper.Name);
        if (index >= 0)
        {
            RenderPaper(Issues[index], CloseButtons[index]);
        }
    }

    private void RenderPaper(GameObject Issue, Button Close)
    {
        Issue.SetActive(true);

        Close.onClick.RemoveAllListeners();
        Close.onClick.AddListener(() =>
        {
            EventManager.Instance.Publish(new GameEvent() { Type = GameEventType.NEXT_DAILY_EVENT });
            Issue.SetActive(false);

            Close.onClick.RemoveAllListeners();
            Close.onClick.AddListener(() => Issue.SetActive(false));
        });
    }
}
