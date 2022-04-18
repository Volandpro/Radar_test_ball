using Infrastructure.Input;
using Player;
using Zenject;

namespace Infrastructure.Services
{
    public class SwitchInputService
    {
        private DiContainer container;
        private PlayerMoveLeftRight player;
        private IPlayerNearBordersChecker playerNearBordersChecker;
        private int currentType = 0;
        
        [Inject]
        public SwitchInputService(DiContainer container, PlayerMoveLeftRight player, IPlayerNearBordersChecker playerNearBordersChecker)
        {
            this.container = container;
            this.player = player;
            this.playerNearBordersChecker = playerNearBordersChecker;
        }

        public void SwitchInput()
        {
            container.Resolve<IInputService>().StopUsing();
            IInputService inputService= SetInputService();
            container.Rebind<IInputService>().FromInstance(inputService);
            container.Inject(player);
            container.Inject(playerNearBordersChecker);
        }

        private IInputService SetInputService()
        {
            IInputService inputService;
            if (currentType == 0)
            {
                inputService = container.Instantiate<AutomaticInputService>();
                currentType = 1;
            }
            else
            {
                inputService = new MobileInputService();
                currentType = 0;
            }

            return inputService;
        }
    }
}