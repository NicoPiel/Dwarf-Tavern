using UnityEngine;

namespace Interactions
{
    public abstract class Interactable : MonoBehaviour
    {
        protected abstract void OnInteract(GameObject source);
        
        public void Interact(GameObject source)
        {
            InteractionEventPayload payload = new InteractionEventPayload(source, this);
            GameManager.GetEventHandler().onPlayerInteract.Invoke(payload);
            if(!payload.Cancelled)
                OnInteract(source);
        }
    }
}
