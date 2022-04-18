using UnityEngine;

namespace Spawn
{
    [RequireComponent(typeof(VisibilityChecker))]
    public class SpawnPiece : MonoBehaviour
    {
        private VisibilityChecker visibilityChecker;
        private ITeleporter teleporter;
        [SerializeField] private int index;
        private void Start()
        {
            visibilityChecker = this.GetComponent<VisibilityChecker>();
            visibilityChecker.BecameInvisible += BecameInvisible;
        }
        public void SetIndex(int index) => 
            this.index = index;
        private void BecameInvisible() => 
            teleporter.PieceBecameInvisible(index);
        public void SetTeleporter(ITeleporter groundTeleporter) => 
            this.teleporter = groundTeleporter;
    }
}