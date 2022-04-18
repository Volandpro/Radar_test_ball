using Infrastructure;
using Infrastructure.Misc;
using UnityEngine;
using Zenject;

namespace Player
{
    [RequireComponent(typeof(TriggerObserver))]
    public class PlayerWallsCollisionActor : MonoBehaviour
    {
        private const int DamageWhenHitWall = 1;
        private TriggerObserver triggerObserver;
        private PlayerHealth playerHealth;

        [Inject]
        public void Construct(PlayerHealth playerHealth) => 
            this.playerHealth = playerHealth;
        private void Start()
        {
            triggerObserver = this.GetComponent<TriggerObserver>();
            triggerObserver.TriggerEntered += WasCollision;
        }

        private void WasCollision(Collider other)
        {
            if (other.gameObject.layer == CachedParameters.WallsLayerNumber)
            {
                playerHealth.MinusHealth(DamageWhenHitWall);
            }
        }
    }
}