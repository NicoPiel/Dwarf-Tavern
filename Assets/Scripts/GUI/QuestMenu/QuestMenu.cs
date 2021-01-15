using UnityEngine;

public class QuestMenu : MonoBehaviour
{
    public QuestMenuEnabler questMenuEnabler;
    
    public void ShowMenu()
    {
        //LeanTween.scale(GetComponent<RectTransform>(), new Vector2(0, 0), 0);
        gameObject.SetActive(true);
        //LeanTween.scale(GetComponent<RectTransform>(), new Vector2(1f, 1f), 0.5f).setEase(LeanTweenType.easeOutQuad);
    }

    public void HideMenu()
    {
        //LeanTween.scale( GetComponent<RectTransform>(), new Vector2(0,0), 0.5f ).setEase(LeanTweenType.easeInQuad).setOnComplete(() =>
        //{
            gameObject.SetActive(false);
            questMenuEnabler.ShowButton();
        //});
    }
}
