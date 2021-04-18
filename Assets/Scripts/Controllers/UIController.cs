using System.Collections;
using AirSeaBattle.Util;
using UnityEngine;
using UnityEngine.UI;

namespace AirSeaBattle.Controllers
{
    public class UIController : MonoBehaviour
    {
        
#pragma warning disable 649
        [SerializeField] 
        private Color _defaultColor;
        
        [SerializeField] 
        private Color _warningColor;
        
        [SerializeField] 
        private Color _hurryColor;
        
        [SerializeField]
        private Text _timerText;
    
        [SerializeField]
        private Text[] _scoreText;

        [SerializeField]
        private Text[] _highscoreText;

        [SerializeField] 
        private GameObject _mainMenu;

        [SerializeField]
        private float _highscoreFXDuration;
        [SerializeField]
        private AnimationCurve _highscoreFxcCurve;
#pragma warning restore 649

        private Coroutine _highscoreRoutine;
        
        private void Awake()
        {
            ReferenceUtil.TestNullReferences(_timerText, "Text to timer", "UIController");
            ReferenceUtil.TestNullReferences(_scoreText, "Text to score", "UIController");
            ReferenceUtil.TestNullReferences(_highscoreText, "Text to highscore", "UIController");
        }

        public void UpdateScores(int score, int highscore)
        {
            UpdateTextArray(_scoreText, score.ToString());
            UpdateTextArray(_highscoreText, highscore.ToString());
        }

        private void UpdateTextArray(Text[] array, string value)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i].text = value;
            }
        }

        public void HighScoreFX()
        {
            if (_highscoreRoutine != null)
            {
                StopCoroutine(_highscoreRoutine);
            }

            _highscoreRoutine = StartCoroutine(FXRoutine());
        }

        private IEnumerator FXRoutine()
        {
            float timer = Time.time + _highscoreFXDuration;
            while (timer >= Time.time)
            {
                for (int i = 0; i < _highscoreText.Length; i++)
                {
                    _highscoreText[i].transform.localScale = Vector3.one * _highscoreFxcCurve.Evaluate((timer -Time.time) / _highscoreFXDuration);
                }
                yield return null;
            }
            for (int i = 0; i < _highscoreText.Length; i++)
            {
                _highscoreText[i].transform.localScale = Vector3.one;
            }
        }

        public void EnableMainMenu(bool enable)
        {
            _mainMenu.SetActive(enable);
        }

        /// <summary>
        /// Updates time UI.
        /// </summary>
        /// <param name="secondsToEnd">Seconds to end the game.</param>
        /// <param name="remainingTimeNormalized">A value between 0 to 1.</param>
        public void UpdateTime(int secondsToEnd, float remainingTimeNormalized)
        {
            _timerText.text = secondsToEnd.ToString();
            if (remainingTimeNormalized >= 0.5f)
            {
                _timerText.color = _defaultColor;
                return;
            }

            if (remainingTimeNormalized >= 0.25f)
            {
                _timerText.color = _warningColor;
                return;
            }
            
            _timerText.color = _hurryColor;
        }
    }
}
