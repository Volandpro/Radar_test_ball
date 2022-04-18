using UI;
using UnityEngine;

namespace Zenject.ProjectContext
{
    public class LoadingCurtainInstaller : MonoInstaller
    {
        [SerializeField] private LoadingCurtain loadingCurtainPrefab;
        public override void InstallBindings()
        {
            LoadingCurtain loadingCurtain = Container.InstantiatePrefabForComponent<LoadingCurtain>(loadingCurtainPrefab);
            Container.Bind<LoadingCurtain>().FromInstance(loadingCurtain).AsSingle();
        }
    }
}
