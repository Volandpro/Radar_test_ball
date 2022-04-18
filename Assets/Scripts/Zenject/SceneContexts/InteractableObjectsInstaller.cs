using Infrastructure.Services;
using Spawn.InteractableObjects;
using UnityEngine;

namespace Zenject.SceneContexts
{
    public class InteractableObjectsInstaller : MonoInstaller
    {
        [SerializeField] private GameObject badObject;
        [SerializeField] private GameObject goodObject;
        public override void InstallBindings()
        {
            InstallPositionChooser();
            InstallFactories();
            InstallSpawner();
        }

        private void InstallSpawner()
        {
            InteractableObjectsSpawner interactableObjectsSpawner = Container.Instantiate<InteractableObjectsSpawner>();
            Container.Bind<InteractableObjectsSpawner>().FromInstance(interactableObjectsSpawner).AsSingle();
        }

        private void InstallFactories()
        {
            Container.BindFactory<BadObject, BadObject.Factory>().FromComponentInNewPrefab(badObject);
            Container.BindFactory<GoodObject, GoodObject.Factory>().FromComponentInNewPrefab(goodObject);
        }

        private void InstallPositionChooser()
        {
            IInteractableObjectPositionChooser positionChooser =
                Container.Instantiate<InteractableObjectPositionChooser>();
            Container.Bind<IInteractableObjectPositionChooser>().FromInstance(positionChooser).AsSingle();
        }
    }
}