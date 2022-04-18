using System;
using UnityEngine;

namespace Player
{
    public class PlayerComponentsEnabler : MonoBehaviour
    {
        public Action OnEnabled;
        public Action OnDisabled;
        
        public void EnableComponents()
        {
            OnEnabled?.Invoke();
        }

        public void DisableComponents()
        {
            OnDisabled?.Invoke();
        }
    }
}
