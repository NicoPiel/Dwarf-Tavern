using UnityEngine;

public class QuestMenuEnabler : MonoBehaviour
{
    public QuestMenu questMenu;
    
    public void ShowQuestMenu()
    {
        questMenu.ShowMenu();
        HideButton();
    }

    public void ShowButton()
    {
        gameObject.SetActive(true);
    }
    
    private void HideButton()
    {
        gameObject.SetActive(false);
    }
}
