using System;
using UnityEngine;

namespace Infrastructure.Misc
{
    public class TriggerObserver : MonoBehaviour
    {
        public Action<Collider> TriggerEntered;

        public void OnTriggerEnter(Collider other)
        {
            TriggerEntered?.Invoke(other);
        }
    }
}