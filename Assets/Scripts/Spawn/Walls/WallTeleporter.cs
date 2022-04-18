using System.Collections.Generic;
using Infrastructure;
using UnityEngine;
using Zenject;

namespace Spawn.Walls
{
    public class WallTeleporter : ITeleporter
    {
        private List<SpawnPiece>[] allPieces { get; set; }
        private WallPositionCalculator positionCalculator;
        private WallRotationCalculator rotationCalculator;

        private Vector3 initialSpawnPoint;
        private bool canTeleport;

        [Inject]
        public WallTeleporter(WallPositionCalculator positionCalculator,WallRotationCalculator rotationCalculator)
        {
            this.positionCalculator = positionCalculator;
            this.rotationCalculator = rotationCalculator;
        }
        public void SetWallsFromSpawner(IWallSpawner wallSpawner)
        {
            allPieces = wallSpawner.allPieces;
            initialSpawnPoint = wallSpawner.spawnPoint;
        }

        public void PieceBecameInvisible(int index) => 
            Teleport(index);
        
        public void Enable() => 
            canTeleport = true;

        public void Disable() => 
            canTeleport = false;
        private void Teleport(int indexInList)
        {
            if(!canTeleport)
                return;
            
            int indexInArray = CalculateIndexInArray(indexInList);
            indexInList = Mathf.Abs(indexInList)-1;
            Transform previousWall = allPieces[indexInArray][CalculatePreviousIndex(indexInArray, indexInList)].transform;
            
            Quaternion newRotation = SetRotation(indexInList, indexInArray, previousWall);
            SetPosition(indexInList, newRotation, previousWall, indexInArray);
        }

        private void SetPosition(int index, Quaternion newRotation, Transform previousWall, int indexInArray)
        {
            Vector3 newPosition =
                positionCalculator.CalculatePosition(Vector3.up * initialSpawnPoint.y, newRotation, previousWall, indexInArray);
            allPieces[indexInArray][index].transform.position = newPosition;
        }

        private Quaternion SetRotation(int index, int indexInArray, Transform previousWall)
        {
            Quaternion newRotation = rotationCalculator.CalculateRotation(indexInArray, previousWall, initialSpawnPoint);
            allPieces[indexInArray][index].transform.rotation = newRotation;
            return newRotation;
        }

        private int CalculateIndexInArray(int index) => 
            index > 0 ? 1 : 0;

        private int CalculatePreviousIndex(int indexInArray,int index)
        {
            int indexOfPreviousPiece = index - 1;
            if (indexOfPreviousPiece < 0)
                indexOfPreviousPiece = allPieces[indexInArray].Count - 1;
            return indexOfPreviousPiece;
        }
    }
}