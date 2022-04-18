using System.Collections;
using Infrastructure.Input;
using Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerNearBordersChecker : IPlayerNearBordersChecker
    {
        private const float Offset=150;
        private float moveModificator=1;
        private ICoroutineRunner coroutineRunner;
        private PlayerComponentsEnabler player;

        [Inject] private IInputService inputService;

        public float MoveModificator
        {
            get { return moveModificator; }
        }


        public PlayerNearBordersChecker(ICoroutineRunner coroutineRunner, IInputService inputService)
        {
            this.inputService = inputService;
            if (this.coroutineRunner == null)
            {
                this.coroutineRunner = coroutineRunner;
                coroutineRunner.StartCoroutine(CheckForBorders());
            }
        }

        public void SetPlayer(PlayerComponentsEnabler player) => 
            this.player = player;

        private IEnumerator CheckForBorders()
        {
            while (true)
            {
                if (player != null)
                {
                    float currentDelta = CalculateCurrentDelta();
                    if (CalculateCurrentPosition(currentDelta) < Offset)
                    {
                        if (Mathf.Sign(inputService.Axis.x) != Mathf.Sign(currentDelta))
                            moveModificator = 0;
                        else
                            moveModificator = 1;
                    }
                }
                yield return null;
            }
        }

        private float CalculateCurrentDelta()
        {
            return Screen.width / 2f -
                   UnityEngine.Camera.main.WorldToScreenPoint(player.transform.position).x;
        }

        private float CalculateCurrentPosition(float currentDelta)
        {
            return Screen.width/2f-Mathf.Abs( currentDelta);
        }
    }
}