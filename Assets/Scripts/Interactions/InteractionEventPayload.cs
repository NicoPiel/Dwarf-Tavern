using UnityEngine;

namespace Interactions
{
    public struct InteractionEventPayload
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
    }
}