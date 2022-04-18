using System.Collections;
using System.Collections.Generic;
using Infrastructure;
using Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace Player
{
    public class ScoreSpeedChanger
    {
        private readonly ICoroutineRunner coroutineRunner;
        private const float TimeToIncrease = 2f;
        private const float InitialSpeedMod = 1;
        public float currentSpeedMod;
        public Dictionary<int, float> speedCoefficients = new Dictionary<int, float>()
        {
            [10] = 1.5f,
            [25] = 2f,
            [50] = 3f,
            [100] = 4f,
        };

        [Inject]
        public ScoreSpeedChanger(Score score,ICoroutineRunner coroutineRunner)
        {
            this.coroutineRunner = coroutineRunner;
            score.OnScoreChanged += CheckToModifySpeed;
        }

        private void CheckToModifySpeed(int currentScore)
        {
            if (currentScore == 0)
            {
                currentSpeedMod = InitialSpeedMod;
                return;
            }

            if (speedCoefficients.ContainsKey(currentScore))
                coroutineRunner.StartCoroutine(IncreaseSpeed(speedCoefficients[currentScore]));
        }

        private IEnumerator IncreaseSpeed(float speedCoefficient)
        {
            float currentTimer = 0f;
            float initialSpeedMod = currentSpeedMod;
            while (currentTimer < TimeToIncrease)
            {
                yield return null;
                currentTimer += Time.deltaTime;
                currentSpeedMod = Mathf.Lerp(initialSpeedMod, speedCoefficient, currentTimer / TimeToIncrease);
            }
        }
    }
}