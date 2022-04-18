using UnityEngine;

namespace Zenject.SceneContexts
{
    public class HudInstaller : MonoInstaller
    {
        [SerializeField] private GameObject hud;
        public override void InstallBindings()
        {
           Container.InstantiatePrefab(hud);
        }
    }
}