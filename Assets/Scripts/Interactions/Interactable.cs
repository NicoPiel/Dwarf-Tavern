using UnityEngine;

namespace Interactions
{
    public abstract class Interactable : MonoBehaviour
    {
        protected abstract void OnInteract();
        
        public void Interact(GameObject source)
        {
            InteractionEventPayload payload = new InteractionEventPayload(source, this);
            GameManager.GetEventHandler().onPlayerInteract.Invoke(payload);
            if(!payload.Cancelled)
                OnInteract();
        }
    }
}
