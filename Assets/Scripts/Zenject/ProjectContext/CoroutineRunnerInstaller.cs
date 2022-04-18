using Infrastructure;
using Infrastructure.Services;
using UnityEngine;

namespace Zenject.ProjectContext
{
    public class CoroutineRunnerInstaller : MonoInstaller
    {
        [SerializeField] private GameObject coroutineRunnerPrefab;
        public override void InstallBindings()
        {
            ICoroutineRunner coroutineRunner = CreateCoroutineRunner();
            Container.Bind<ICoroutineRunner>().FromInstance(coroutineRunner).AsSingle();
        }
        private ICoroutineRunner CreateCoroutineRunner() => 
            Container.InstantiatePrefabForComponent<ICoroutineRunner>(coroutineRunnerPrefab,Vector3.zero, Quaternion.identity,null);
    }
}
