using UnityEngine;

public class BeerDisplay : BaseMenu
{
    public override void ShowMenu()
    {
        gameObject.SetActive(true);
        
        LeanTween.move(GetComponent<RectTransform>(), new Vector3(0, 200, 0), 0.75f);
    }

    public override void HideMenu()
    {
        LeanTween.move(GetComponent<RectTransform>(), new Vector3(0, -200, 0), 0.75f).setOnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
}
