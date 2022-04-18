using Player;
using UnityEngine;
using Zenject;

namespace Spawn.InteractableObjects
{
    public class BadObject : InteractableObject
    {
        private PlayerHealth playerHealth;
        private const int damage = 1;

        [Inject]
        public void Construct(PlayerHealth playerHealth)
        {
            this.playerHealth = playerHealth;
        }
        protected override void TriggerEntered(Collider other)
        {
            playerHealth.MinusHealth(damage);
            base.TriggerEntered(other);
        }
        public class Factory : PlaceholderFactory<BadObject>
        {
        }
    }
}