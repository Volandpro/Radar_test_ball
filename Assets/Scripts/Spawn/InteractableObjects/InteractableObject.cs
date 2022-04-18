using Infrastructure.Misc;
using UnityEngine;

namespace Spawn.InteractableObjects
{
    [RequireComponent(typeof(TriggerObserver))]
    public abstract class InteractableObject : MonoBehaviour
    {
        private TriggerObserver triggerObserver;
        private void Start()
        {
            triggerObserver = this.GetComponent<TriggerObserver>();
            triggerObserver.TriggerEntered += TriggerEntered;
        }

        protected virtual void TriggerEntered(Collider other)
        {
            Destroy(this.gameObject);
        }
     
    }
}