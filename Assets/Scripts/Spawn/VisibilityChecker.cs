using System;
using UnityEngine;

namespace Spawn
{
    public class VisibilityChecker : MonoBehaviour
    {
        public Action BecameInvisible;
        public void OnBecameInvisible() => 
            BecameInvisible?.Invoke();
    }
}