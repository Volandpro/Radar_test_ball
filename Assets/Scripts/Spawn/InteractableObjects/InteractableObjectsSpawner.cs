using UnityEngine;
using Zenject;

namespace Spawn.InteractableObjects
{
    public class InteractableObjectsSpawner : ISpawner
    {
        private BadObject.Factory badObjectsFactory;
        private GoodObject.Factory goodObjectsFactory;
        private readonly TimerForSpawnInteractableObjects timerForSpawnInteractableObjects;
        private readonly IInteractableObjectPositionChooser positionChooser;

        [Inject]
        public InteractableObjectsSpawner(TimerForSpawnInteractableObjects timerForSpawnInteractableObjects,IInteractableObjectPositionChooser positionChooser, 
            BadObject.Factory badObjectsFactory, GoodObject.Factory goodObjectsFactory)
        {
            this.timerForSpawnInteractableObjects = timerForSpawnInteractableObjects;
            this.positionChooser = positionChooser;
            this.badObjectsFactory = badObjectsFactory;
            this.goodObjectsFactory = goodObjectsFactory;
            this.timerForSpawnInteractableObjects.OnTimer += Spawn;
        }


        public void Spawn()
        {
            InteractableObject spawnedObject;
            if (Random.Range(0, 2) == 0)
                spawnedObject=badObjectsFactory.Create();
            else
                spawnedObject=goodObjectsFactory.Create();
            
            spawnedObject.transform.position=positionChooser.GetPosition();;
        }

    }
}