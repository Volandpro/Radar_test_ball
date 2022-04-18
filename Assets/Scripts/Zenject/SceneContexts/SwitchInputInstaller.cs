using Infrastructure;
using Infrastructure.Services;
using UnityEngine;

namespace Zenject.SceneContexts
{
    public class SwitchInputInstaller : MonoInstaller
    {
        [SerializeField] private ClickChecker clickCheckerPrefab;
        public override void InstallBindings()
        {
            InstallSwitchInputService();
            InstallClickChecker();
        }

        private void InstallClickChecker()
        {
            ClickChecker clickChecker = Container.InstantiatePrefabForComponent<ClickChecker>(clickCheckerPrefab);
            Container.Bind<ClickChecker>().FromInstance(clickChecker).AsSingle();
        }

        private void InstallSwitchInputService()
        {
            SwitchInputService switchInputService = Container.Instantiate<SwitchInputService>();
            Container.Bind<SwitchInputService>().FromInstance(switchInputService).AsSingle();
        }
    }
}