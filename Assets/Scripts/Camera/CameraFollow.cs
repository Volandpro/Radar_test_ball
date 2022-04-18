using Player;
using UnityEngine;
using Zenject;

namespace Camera
{
    public class CameraFollow : MonoBehaviour
    {
        private const float Offset = -5f;
        [SerializeField] private Transform targetToFollow;
        private Vector3 startPosition;

        [Inject]
        private void Construct(PlayerComponentsEnabler player)
        {
            targetToFollow = player.transform;
        }
        private void Start() => 
            startPosition = this.transform.position;

        void LateUpdate() => 
            this.transform.position =  CalculateNewPosition();

        private Vector3 CalculateNewPosition() => 
            new Vector3(startPosition.x, startPosition.y, targetToFollow.position.z + Offset);
    }
}
