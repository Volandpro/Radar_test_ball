using System;
using System.Collections.Generic;
using Infrastructure.ActualProviders;
using Infrastructure.States;
using Infrastructure.States.GameLoopStates;
using Infrastructure.States.GameStates;

namespace Zenject.SceneContexts
{
    public class GameLoopStatesInstaller : MonoInstaller
    {
        private Dictionary<Type, IState> allStates;
        [Inject] private ActualGameLoopStateMachineProvider gameLoopStatesProvider;
        public override void InstallBindings()
        {
            StatesContainer statesContainer = InstallStatesContainer();
            GameLoopStateMachine gameLoopStateMachine = InstallStaGameLoopStateMachine();
            InstallStates();

            statesContainer.allStates = allStates;
            gameLoopStatesProvider.SetStateMachine(gameLoopStateMachine);
        }

        private StatesContainer InstallStatesContainer()
        {
            StatesContainer statesContainer = Container.Instantiate<StatesContainer>();
            Container.Bind<StatesContainer>().FromInstance(statesContainer).AsSingle();
            return statesContainer;
        }

        private GameLoopStateMachine InstallStaGameLoopStateMachine()
        {
            GameLoopStateMachine gameLoopStateMachine = Container.Instantiate<GameLoopStateMachine>();
            Container.Bind<GameLoopStateMachine>().FromInstance(gameLoopStateMachine).AsSingle();
            return gameLoopStateMachine;
        }

        private void InstallStates()
        {
            allStates = new Dictionary<Type, IState>()
            {
                [typeof(LevelCreationState)] = Container.Instantiate<LevelCreationState>(),
                [typeof(PlayingState)] = Container.Instantiate<PlayingState>(),
                [typeof(DeathState)] = Container.Instantiate<DeathState>(),
            };
            Container.Bind<Dictionary<Type, IState>>().FromInstance(allStates).AsSingle();
        }
    }
}