using UnityEngine;

namespace Interactions
{
    public abstract class Interactable : MonoBehaviour
    {
        protected abstract void OnInteract(GameObject source);
        
        public void Interact(GameObject source)
        {
            InteractionEventPayload payload = new InteractionEventPayload(source, this);
            EventHandler.onInteraction.Invoke(payload);
            Debug.Log(payload.ToString());
            if(!payload.Cancelled)
                OnInteract(source);
        }
        
        public class InteractionEventPayload
        {

            public InteractionEventPayload(GameObject source, Interactable target)
            {
                Source = source;
                Target = target;
                Cancelled = false;
            }
        
            public GameObject Source { get; }
            public Interactable Target { get; }
            public bool Cancelled { get; set; }

            public override string ToString()
            {
                return "Source: " + (Source != null ? Source.ToString() : "null") + " Target: " +
                       (Target != null ? Target.ToString() : "null") + " Cancelled: " + Cancelled;
            }
            
        }
    }
}
