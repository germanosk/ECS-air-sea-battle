namespace AirSeaBattle.Controllers
{
    public class ScoreController
    {
        private int _highScore;
        private int _score;
        private int _scorePerPlane;

        public int Score => _score;
        public int Highscore => _highScore;

        public ScoreController(int highScore, int scorePerPlane)
        {
            _highScore = highScore;
            _score = 0;
            _scorePerPlane = scorePerPlane;
        }

        public void UpdateReferenceValues(int highScore, int scorePerPlane)
        {
            // To avoid division by zero
            _scorePerPlane = _scorePerPlane == 0 ? 1 : _scorePerPlane;
            // We recalculate the score using the new values in case of
            // updating the reference values while a game is being played
            _score = (_score / _scorePerPlane) * scorePerPlane;
            _scorePerPlane = scorePerPlane;
            _highScore = highScore;
        }

        /// <summary>
        /// Updates the score and returns true if player achieved a new highscore.
        /// </summary>
        /// <returns>True if a new highscore was achieved.</returns>
        public bool AirplaneDestroyed()
        {
            _score += _scorePerPlane;
            bool newHighscore = _score > _highScore;
            _highScore = newHighscore ? _score : _highScore;
            return newHighscore;
        }

        /// <summary>
        /// Resets the current score;
        /// </summary>
        public void ResetScore()
        {
            _score = 0;
        }
    }
}