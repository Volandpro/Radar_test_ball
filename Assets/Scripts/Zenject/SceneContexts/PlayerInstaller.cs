using Infrastructure.Services;
using Player;
using UnityEngine;

namespace Zenject.SceneContexts
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private GameObject player;
        [SerializeField] private PlayerSpawnPoint playerSpawnPoint;

        public override void InstallBindings()
        {
            PlayerNearBordersChecker playerNearBordersChecker = InstallPlayerBordersChecker();
            InstallPlayerHealth();
            PlayerComponentsEnabler player = InstallPlayerComponents();
            InstallPlayerSpawnPoint();
            playerNearBordersChecker.SetPlayer(player);
        }

        private void InstallPlayerSpawnPoint()
        {
            Container.Bind<PlayerSpawnPoint>().FromInstance(playerSpawnPoint);
        }

        private PlayerComponentsEnabler InstallPlayerComponents()
        {
            PlayerComponentsEnabler player = CreateHero();
            Container.Bind<PlayerComponentsEnabler>().FromInstance(player).AsSingle();
            Container.Bind<PlayerMoveLeftRight>().FromInstance(player.GetComponent<PlayerMoveLeftRight>()).AsSingle();
            return player;
        }

        private void InstallPlayerHealth()
        {
            PlayerHealth playerHealth = Container.Instantiate<PlayerHealth>();
            Container.Bind<PlayerHealth>().FromInstance(playerHealth);
        }

        private PlayerNearBordersChecker InstallPlayerBordersChecker()
        {
            PlayerNearBordersChecker playerNearBordersChecker = Container.Instantiate<PlayerNearBordersChecker>();
            Container.Bind<IPlayerNearBordersChecker>().FromInstance(playerNearBordersChecker).AsSingle();
            return playerNearBordersChecker;
        }

        private PlayerComponentsEnabler CreateHero() => 
            Container.InstantiatePrefabForComponent<PlayerComponentsEnabler>(player,playerSpawnPoint.transform.position,Quaternion.identity,null);
    }
}
    

   


