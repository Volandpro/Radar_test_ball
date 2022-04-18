using UnityEngine;
using Zenject;

namespace Player
{
    [RequireComponent(typeof(PlayerComponentsEnabler))]
    public class PlayerMoveUp : MonoBehaviour
    {
        private const float MoveSpeed = 5f;
        private PlayerComponentsEnabler playerComponents;
        private ScoreSpeedChanger speedChanger;

        [Inject]
        public void Construct(ScoreSpeedChanger speedChanger) => 
            this.speedChanger = speedChanger;

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
            enabled = true;

        private void DisableThis() => 
            enabled = false;

        void Update() => 
            transform.position +=CalculateMoveVector();

        private Vector3 CalculateMoveVector() => 
            Vector3.forward*Time.deltaTime*MoveSpeed*speedChanger.currentSpeedMod;
    }
}
