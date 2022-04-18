using Infrastructure;
using Infrastructure.ActualProviders;
using Infrastructure.Services;
using Spawn;
using Spawn.Ground;
using UnityEngine;

namespace Zenject.SceneContexts
{
    public class GroundInstaller : MonoInstaller
    {
        public GameObject groundPrefab;
        [Inject] private ActualSpawnersProvider spawnersProvider;
        public override void InstallBindings()
        {
            GroundTeleporter groundTeleporter = InstallGroundTeleporter();
            IGroundSpawner groundSpawner = InstallGroundSpawner();
            groundTeleporter.SetGroundPiecesFromSpawner(groundSpawner);
        }

        private IGroundSpawner InstallGroundSpawner()
        {
            IGroundSpawner groundSpawner = Container.Instantiate<GroundSpawner>();
            groundSpawner.SetPrefab(groundPrefab);
            spawnersProvider.AddSpawner(groundSpawner);
            Container.Bind<IGroundSpawner>().FromInstance(groundSpawner).AsSingle();
            return groundSpawner;
        }

        private GroundTeleporter InstallGroundTeleporter()
        {
            GroundTeleporter groundTeleporter = Container.Instantiate<GroundTeleporter>();
            Container.Bind<GroundTeleporter>().FromInstance(groundTeleporter).AsSingle();
            return groundTeleporter;
        }
    }
}