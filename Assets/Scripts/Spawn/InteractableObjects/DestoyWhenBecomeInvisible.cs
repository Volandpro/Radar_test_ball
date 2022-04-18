using UnityEngine;

namespace Spawn.InteractableObjects
{
    public class DestoyWhenBecomeInvisible : MonoBehaviour
    {
        private void OnBecameInvisible() =>
            Destroy(this.gameObject);
    }
}