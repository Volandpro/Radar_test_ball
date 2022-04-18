using Player;
using UnityEngine;
using Zenject;

namespace Spawn.InteractableObjects
{
    public class GoodObject : InteractableObject
    {
        private const int ScoreToAdd = 1;
        private Score score;
        
        [Inject]
        public void Construct(Score score) => 
            this.score = score;

        protected override void TriggerEntered(Collider other)
        {
            score.AddScore(ScoreToAdd);
            base.TriggerEntered(other);
        }
        public class Factory : PlaceholderFactory<GoodObject>
        {
        }
    }
}