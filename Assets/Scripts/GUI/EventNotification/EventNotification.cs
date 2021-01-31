using UnityEngine;

public class EventNotification : BaseMenu
{
    public AudioSource audioSource;

    private void Start()
    {
        EventHandler.onTriggerExpeditionEvent.AddListener(ShowMenuOnEventTrigger);
        EventHandler.onExpeditionEventFinished.AddListener(HideMenu);
    }

    public override void ShowMenu()
    {
        isShown = true;
        
        LeanTween.move(GetComponent<RectTransform>(), new Vector3(-200, 45, 0), 0.4f).setEaseInBounce();
    }

    public override void HideMenu()
    {
        isShown = false;
        
        LeanTween.move(GetComponent<RectTransform>(), new Vector3(230, 45, 0), 0.4f).setEaseOutBounce();
    }

    public void ShowMenuOnEventTrigger(Expeditions.Events.Event triggeredEvent)
    {
        audioSource.Play();
        ShowMenu();
    }
}
