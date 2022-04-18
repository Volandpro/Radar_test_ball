using Infrastructure.ActualProviders;
using Infrastructure.Services;
using Spawn;
using Zenject.SceneContexts;

namespace Zenject.ProjectContext
{
    public class ActualProvidersInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallGameLoopStateMachineProvider();
            InstallSpawnersProvider();
        }

        private void InstallSpawnersProvider()
        {
            ActualSpawnersProvider spawnersProvider = Container.Instantiate<ActualSpawnersProvider>();
            Container.Bind<ActualSpawnersProvider>().FromInstance(spawnersProvider).AsSingle();
        }

        private void InstallGameLoopStateMachineProvider()
        {
            ActualGameLoopStateMachineProvider gameLoopStateProvider =
                Container.Instantiate<ActualGameLoopStateMachineProvider>();
            Container.Bind<ActualGameLoopStateMachineProvider>().FromInstance(gameLoopStateProvider).AsSingle();
        }
    }
}