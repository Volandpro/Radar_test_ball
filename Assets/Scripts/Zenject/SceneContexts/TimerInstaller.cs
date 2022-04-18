using Spawn.InteractableObjects;
using UnityEngine;

namespace Zenject.SceneContexts
{
    public class TimerInstaller : MonoInstaller
    {
        [SerializeField] private GameObject timerPrefab;
        public override void InstallBindings()
        {
            TimerForSpawnInteractableObjects timerForSpawnInteractableObjects = 
                Container.InstantiatePrefabForComponent<TimerForSpawnInteractableObjects>(timerPrefab);
            Container.Bind<TimerForSpawnInteractableObjects>().FromInstance(timerForSpawnInteractableObjects).AsSingle();
        }
    }
}