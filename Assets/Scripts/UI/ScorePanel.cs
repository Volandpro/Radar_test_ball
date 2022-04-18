using Player;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI
{
    public class ScorePanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;

        [Inject]
        public void Construct(Score score) => 
            score.OnScoreChanged += UpdateLabel;

        private void UpdateLabel(int value) => 
            scoreText.text = value.ToString();
    }
}