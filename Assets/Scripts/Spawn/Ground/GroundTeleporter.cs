using System.Collections.Generic;
using Infrastructure;
using Infrastructure.Misc;
using UnityEngine;

namespace Spawn.Ground
{
    public class GroundTeleporter : ITeleporter
    {
        private List<SpawnPiece> allGroundPieces = new List<SpawnPiece>();
        private bool canTeleport;
        public void SetGroundPiecesFromSpawner(IGroundSpawner groundSpawner) => 
            allGroundPieces = groundSpawner.allPieces;

        public void PieceBecameInvisible(int index) => 
            Teleport(index);
        
        public void Enable() => 
            canTeleport = true;

        public void Disable() => 
            canTeleport = false;
        private void Teleport(int index)
        {
            if(!canTeleport)
                return;
            
            allGroundPieces[index].transform.position =
                allGroundPieces[CalculatePreviousIndex(index)].transform.position + Vector3.forward * CachedParameters.groundLength;
        }

        private int CalculatePreviousIndex(int index)
        {
            int indexOfPreviousPiece = index - 1;
            if (indexOfPreviousPiece < 0)
                indexOfPreviousPiece = allGroundPieces.Count - 1;
            return indexOfPreviousPiece;
        }
    }
}