using System;
using Player;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Spawn.InteractableObjects
{
    public class TimerForSpawnInteractableObjects : MonoBehaviour
    {
        private float currentTimer;
        private ScoreSpeedChanger speedChanger;
        private float maxTimer;
        private const float initialMaxTimer=1;
        private const float minRandom=0.8f;
        private const float maxRandom=1.2f;

        public Action OnTimer;

        [Inject]
        public void Construct(ScoreSpeedChanger speedChanger) => 
            this.speedChanger = speedChanger;
        void Update()
        {
            currentTimer += Time.deltaTime;
            if (currentTimer > maxTimer)
            {
                OnTimer?.Invoke();
                currentTimer = 0;
                maxTimer = CalculateMaxTimer();
            }
        }

        private float CalculateMaxTimer() => 
            Random.Range(minRandom,maxRandom)*initialMaxTimer/speedChanger.currentSpeedMod;
    }
}