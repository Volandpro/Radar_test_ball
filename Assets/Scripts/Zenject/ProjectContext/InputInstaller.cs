using Infrastructure.Input;

namespace Zenject.ProjectContext
{
    public class InputInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            IInputService inputService = new MobileInputService();
            Container.Bind<IInputService>().FromInstance(inputService).AsSingle();
        }
    }
}
