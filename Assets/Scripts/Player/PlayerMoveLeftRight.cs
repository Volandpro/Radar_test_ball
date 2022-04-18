using Infrastructure.Input;
using Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace Player
{
    [RequireComponent(typeof(PlayerComponentsEnabler))]
    public class PlayerMoveLeftRight : MonoBehaviour
    {
        private const float MoveSpeed = 5f;
        private IInputService inputService;
        private PlayerComponentsEnabler playerComponents;
        private IPlayerNearBordersChecker bordersChecker;

        [Inject]
        public void Construct(IInputService inputService,  IPlayerNearBordersChecker bordersChecker)
        {
            this.bordersChecker = bordersChecker;
            this.inputService = inputService;
        }

        private void Awake()
        {
            playerComponents = this.GetComponent<PlayerComponentsEnabler>();
            playerComponents.OnDisabled += DisableThis;
            playerComponents.OnEnabled += EnableThis;
        }

        private void OnDestroy()
        {
            playerComponents.OnDisabled -= DisableThis;
            playerComponents.OnEnabled -= EnableThis;
        }
        private void EnableThis() => 
            this.enabled = true;

        private void DisableThis() => 
            this.enabled = false;

        void Update() => 
            transform.position += Vector3.right* (inputService.Axis.x*Time.deltaTime*MoveSpeed*bordersChecker.MoveModificator);
    }
}