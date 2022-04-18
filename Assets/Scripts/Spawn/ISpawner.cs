using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spawn
{
    public interface ISpawner
    {
        void Spawn();
    }

    public interface IInitialSpawner : ISpawner
    {
        Action<IInitialSpawner> OnFinishedSpawn { get; set; }
        Vector3 spawnPoint { get; set; }
        void FinishedSpawn();
        void SetPrefab(GameObject prefab);
    }
    public interface IWallSpawner : IInitialSpawner
    {
        List<SpawnPiece>[] allPieces { get; set; }
    }
    public interface IGroundSpawner : IInitialSpawner
    {
        List<SpawnPiece> allPieces { get; set; }
    }
}