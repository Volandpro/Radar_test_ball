using System;

namespace Player
{
    public class Score
    {
        public int currentScore;
        public Action<int> OnScoreChanged;

        public void AddScore(int value)
        {
            currentScore += value;
            OnScoreChanged(currentScore);
        }

        public void ResetScore()
        {
            currentScore = 0;
            OnScoreChanged(currentScore);
        }
    }
}