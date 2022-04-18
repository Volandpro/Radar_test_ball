using Infrastructure;
using Infrastructure.Services;

namespace Zenject.ProjectContext
{
    public class ServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SceneLoader sceneLoader = Container.Instantiate<SceneLoader>();
            Container.Bind<SceneLoader>().FromInstance(sceneLoader).AsSingle();
        }
    }
}