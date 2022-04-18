using System.Collections;
using Infrastructure;
using Infrastructure.Misc;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(TriggerObserver))]
    [RequireComponent(typeof(PlayerComponentsEnabler))]
    public class PlayerBouncerFromWalls : MonoBehaviour
    {
        private TriggerObserver triggerObserver;
        private PlayerMoveLeftRight moveLeftRight;
        private PlayerComponentsEnabler playerComponents;
        private const float distanceToAdd = 10f;
        private const float timeToAdd = 0.5f;
        private void Awake()
        {
            triggerObserver = this.GetComponent<TriggerObserver>();
            moveLeftRight = this.GetComponent<PlayerMoveLeftRight>();
            triggerObserver.TriggerEntered += WasCollision;
            playerComponents = this.GetComponent<PlayerComponentsEnabler>();
            playerComponents.OnDisabled += DisableThis;
            playerComponents.OnEnabled += EnableThis;
        }

        private void OnDestroy()
        {
            playerComponents.OnDisabled -= DisableThis;
            playerComponents.OnEnabled -= EnableThis;
        }
        private void EnableThis() => 
            enabled = true;

        private void DisableThis() => 
            enabled = false;

        private void WasCollision(Collider other)
        {
            if (other.gameObject.layer == CachedParameters.WallsLayerNumber)
            {
                moveLeftRight.enabled = false;
                AddForce(other.ClosestPoint(this.transform.position).x-this.transform.position.x);
            }
        }

        private void AddForce(float deltaPosition)
        {
            StopAllCoroutines();
            StartCoroutine(AddForceCour(deltaPosition));
        }

        private IEnumerator AddForceCour(float deltaPosition)
        {
           
            float currentDistanceToAdd = distanceToAdd;
            float currTimeToAdd = timeToAdd;
            while (currTimeToAdd > 0)
            {
                transform.position-=Vector3.right*currentDistanceToAdd*Time.deltaTime*Mathf.Sign(deltaPosition);
                currTimeToAdd -= Time.deltaTime;
                currentDistanceToAdd=Mathf.Lerp(0,distanceToAdd,currTimeToAdd/timeToAdd);
                yield return null;
            }

            moveLeftRight.enabled = true;
        }
    }
}