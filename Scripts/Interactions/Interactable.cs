using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    private bool _cancelInteraction;
    protected abstract void OnInteract();

    public void SetCancelled(bool cancelled)
    {
        _cancelInteraction = cancelled;
    }

    public void Interact()
    {
        SetCancelled(false);
        GameManager.GetEventHandler().onPlayerInteract.Invoke(this);
        if(!_cancelInteraction)
            OnInteract();
    }
}
