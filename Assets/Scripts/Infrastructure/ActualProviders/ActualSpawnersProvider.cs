using System;
using System.Collections.Generic;
using Spawn;

namespace Infrastructure.ActualProviders
{
    public class ActualSpawnersProvider
    {
        public Action<List<IInitialSpawner>> OnSpawnersSet;
        private List<IInitialSpawner> allSpawners = new List<IInitialSpawner>();
        
        public void AddSpawner(IInitialSpawner spawnerToAdd) => 
            allSpawners.Add(spawnerToAdd);

        public void InvokeAction() => 
            OnSpawnersSet?.Invoke( allSpawners);
    }
}