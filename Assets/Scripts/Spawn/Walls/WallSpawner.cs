using System;
using System.Collections;
using System.Collections.Generic;
using Infrastructure;
using Infrastructure.Misc;
using Infrastructure.Services;
using Player;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Spawn.Walls
{
    public class WallSpawner : IWallSpawner
    {
        private GameObject wallPrefab;
        private ICoroutineRunner coroutineRunner;
        private ITeleporter wallTeleporter;
        private WallPositionCalculator positionCalculator;
        private WallRotationCalculator rotationCalculator;
        private const int ChunkSize = 5;
        public Vector3 spawnPoint { get; set; }
        public Action<IInitialSpawner> OnFinishedSpawn { get; set; }

        public List<SpawnPiece>[] allPieces { get; set; } = new List<SpawnPiece>[2];

        [Inject]
        public WallSpawner(PlayerComponentsEnabler player, ICoroutineRunner coroutineRunner, 
            WallTeleporter wallTeleporter, WallPositionCalculator positionCalculator,WallRotationCalculator rotationCalculator)
        {
            spawnPoint = player.transform.position;
            this.coroutineRunner = coroutineRunner;
            this.wallTeleporter = wallTeleporter;
            this.positionCalculator = positionCalculator;
            this.rotationCalculator = rotationCalculator;
        }

        public void SetPrefab(GameObject prefab) => 
            wallPrefab = prefab;

        public void Spawn()
        {
            ClearPieces();
            coroutineRunner.StartCoroutine(SpawnOneSide(0));
            coroutineRunner.StartCoroutine(SpawnOneSide(1));
        }
        

        private void ClearPieces()
        {
            for (int i = 0; i < allPieces.Length; i++)
            {
                if (allPieces[i] != null)
                {
                    for (int j = 0; j < allPieces[i].Count; j++)
                    {
                        Object.Destroy(allPieces[i][j].gameObject);
                    }
                }
            }
        }

        private IEnumerator SpawnOneSide(int side)
        {
            allPieces[side] = new List<SpawnPiece>();
            CachedParameters.wallsLength=wallPrefab.GetComponent<MeshFilter>().sharedMesh.bounds.size.z*wallPrefab.transform.localScale.z;
            bool pieceIsVisible = true;
            int iterator = 0;
            Transform previousWallPiece=null;
            while(pieceIsVisible)
            {
                for (int i = 0; i < ChunkSize; i++)
                {
                    Quaternion newRotation = rotationCalculator.CalculateRotation(side, previousWallPiece, spawnPoint);
                    Vector3 newPosition =
                        positionCalculator.CalculatePosition(spawnPoint, newRotation, previousWallPiece, side);
                    SpawnPiece newWallPiece = Object.Instantiate(wallPrefab, newPosition, newRotation)
                        .GetComponent<SpawnPiece>();
                    previousWallPiece = newWallPiece.transform;
                    newWallPiece.SetIndex((int) ((iterator + 1) * (side - 0.5f) * 2));
                    newWallPiece.SetTeleporter(wallTeleporter);
                    allPieces[side].Add(newWallPiece);
                    
                    iterator++;
                }
                yield return new WaitForEndOfFrame();
                pieceIsVisible = previousWallPiece.GetComponent<MeshRenderer>().isVisible || iterator == 0;
            }
            FinishedSpawn();
        }

        public void FinishedSpawn() => 
            OnFinishedSpawn?.Invoke(this);
    }
}