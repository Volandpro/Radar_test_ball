using Infrastructure.Misc;
using Player;
using UnityEngine;
using Zenject;

namespace Spawn.InteractableObjects
{
    public class InteractableObjectPositionChooser : IInteractableObjectPositionChooser
    {
        private const int rayLength = 100;
        private const int SpawnWallOffset=2;
        private readonly PlayerSpawnPoint spawnPoint;
        private readonly PlayerComponentsEnabler player;
        private int layer = 1 << CachedParameters.WallsLayerNumber;

        [Inject]
        public InteractableObjectPositionChooser(PlayerSpawnPoint spawnPoint, PlayerComponentsEnabler player)
        {
            this.spawnPoint = spawnPoint;
            this.player = player;
        }

        public Vector3 GetPosition()
        {
            float zPosition = CalculateZPosition();
            float xPosition = CalculateXPosition(zPosition);
            return new Vector3(xPosition,spawnPoint.transform.position.y,zPosition);
        }

        private float CalculateZPosition()
        {
            return player.transform.position.z + CachedParameters.distanceToSpawn;
        }

        private float CalculateXPosition(float zPosition)
        {
            Vector3 positionToCastRay =
                new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y, zPosition);
            
            float minPosition = CalculateHitPoint(positionToCastRay,-1);
            float maxPosition=CalculateHitPoint(positionToCastRay,1);

            return Random.Range(minPosition,maxPosition);
        }

        private float CalculateHitPoint(Vector3 positionToCastRay, int sign)
        {
            RaycastHit hit;
            Physics.Raycast(positionToCastRay, Vector3.right*sign, out hit, rayLength, layer);
            return hit.point.x+SpawnWallOffset*(-sign);
        }
    }
}