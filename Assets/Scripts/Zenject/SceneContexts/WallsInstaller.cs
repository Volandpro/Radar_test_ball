using Infrastructure;
using Infrastructure.ActualProviders;
using Infrastructure.Services;
using Spawn;
using Spawn.Walls;
using UnityEngine;

namespace Zenject.SceneContexts
{
    public class WallsInstaller : MonoInstaller
    {
        public GameObject wallPrefab;
        [Inject] private ActualSpawnersProvider spawnersProvider;

        public override void InstallBindings()
        {
            InstallWallsPositionCalculator();
            InstallWallsRotationCalculator();
            WallTeleporter wallTeleporter = InstallWallsTeleporter();
            IWallSpawner wallSpawner = InstallWallsSpawner();

            wallTeleporter.SetWallsFromSpawner(wallSpawner);
        }

        private IWallSpawner InstallWallsSpawner()
        {
            IWallSpawner wallSpawner = Container.Instantiate<WallSpawner>();
            wallSpawner.SetPrefab(wallPrefab);
            spawnersProvider.AddSpawner(wallSpawner);
            Container.Bind<IWallSpawner>().FromInstance(wallSpawner).AsSingle();
            return wallSpawner;
        }

        private WallTeleporter InstallWallsTeleporter()
        {
            WallTeleporter wallTeleporter = Container.Instantiate<WallTeleporter>();
            Container.Bind<WallTeleporter>().FromInstance(wallTeleporter).AsSingle();
            return wallTeleporter;
        }

        private void InstallWallsRotationCalculator()
        {
            WallRotationCalculator rotationCalculator = Container.Instantiate<WallRotationCalculator>();
            Container.Bind<WallRotationCalculator>().FromInstance(rotationCalculator).AsSingle();
        }

        private void InstallWallsPositionCalculator()
        {
            WallPositionCalculator positionCalculator = Container.Instantiate<WallPositionCalculator>();
            Container.Bind<WallPositionCalculator>().FromInstance(positionCalculator).AsSingle();
        }
    }
}