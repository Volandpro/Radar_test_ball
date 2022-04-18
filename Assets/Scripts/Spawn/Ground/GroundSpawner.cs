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

namespace Spawn.Ground
{
    public class GroundSpawner : IGroundSpawner
    {
        private GameObject groundPrefab;
        private ICoroutineRunner coroutineRunner;
        private GroundTeleporter groundTeleporter;

        public Vector3 spawnPoint { get; set; }
        public List<SpawnPiece> allPieces { get; set; } = new List<SpawnPiece>();
        public Action<IInitialSpawner> OnFinishedSpawn { get; set; }


        [Inject]
        public GroundSpawner(PlayerComponentsEnabler player, ICoroutineRunner coroutineRunner, GroundTeleporter groundTeleporter)
        {
            spawnPoint = player.transform.position;
            this.coroutineRunner = coroutineRunner;
            this.groundTeleporter = groundTeleporter;
        }

        public void SetPrefab(GameObject groundPrefab) =>
            this.groundPrefab = groundPrefab;

        public void Spawn()
        {
            ClearPieces();
            coroutineRunner.StartCoroutine(SpawnGround());
        }

        private void ClearPieces()
        {
            for (int i = 0; i < allPieces.Count; i++)
            {
                Object.Destroy(allPieces[i].gameObject);
            }
            allPieces.Clear();
        }

        public void FinishedSpawn() => 
            OnFinishedSpawn?.Invoke(this);
        

        private IEnumerator SpawnGround()
        {
            CachedParameters.groundLength = groundPrefab.GetComponent<MeshFilter>().sharedMesh.bounds.size.z;
            bool piceIsVisible = true;
            int iterator = 0;
            while(piceIsVisible)
            {
                SpawnPiece newGroundPiece = Object.Instantiate(groundPrefab, spawnPoint+Vector3.forward*CachedParameters.groundLength*iterator, 
                    Quaternion.identity).GetComponent<SpawnPiece>();
                newGroundPiece.SetIndex(iterator);
                newGroundPiece.SetTeleporter(groundTeleporter);
                allPieces.Add(newGroundPiece);
                yield return new WaitForEndOfFrame();
                piceIsVisible = newGroundPiece.GetComponent<MeshRenderer>().isVisible || iterator == 0;
                iterator++;
            }

            CachedParameters.distanceToSpawn = CachedParameters.groundLength * (iterator-1);
            FinishedSpawn();
        }
    }
}
