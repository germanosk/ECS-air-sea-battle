using AirSeaBattle.Util;
using UnityEngine;

namespace AirSeaBattle.Controllers
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton
        private static GameManager _instance;
        public static GameManager  Instance {
            get {
                if (_instance == null)
                {
                    Debug.LogError("GameManager is missing.");
                }
                return _instance;
            }
        }
        #endregion

        public int MinAiplanesPerWave = 3;
        public int MaxAiplanesPerWave = 5;
        public float AiplaneSpeed = 5.0f;
        
        public int MaxBulletsOnScreen = 5;
        public float BulletSpeed = 5.0f;
       
#pragma warning disable 649 
        [Tooltip("A default value to add to the score for each plane destroyed.")]
        [SerializeField]
        private int _defaultScorePerPlane = 1;

        [Tooltip("A default value to the highscore.")]
        [SerializeField]
        private int _defaultHighscore = 100;
        
        [Tooltip("A default value to the time limit in second.")]
        [SerializeField]
        private int _defaultTimeLimit = 30;

        private int _timeLimit;
        
        [Tooltip("A reference to the SoundController.")]
        [SerializeField]
        private SoundController _soundController;
        
        [Tooltip("A reference to the SoundController.")]
        [SerializeField]
        private UIController _uiController;

        [Tooltip("A reference to the TimeController.")]
        [SerializeField]
        private TimeController _timeController;
        
        [Tooltip("A reference to the CameraController.")]
        [SerializeField]
        private CameraController _cameraController;
        
#pragma warning restore 649
        
        private ScoreController _scoreController;

        private Vector2 _minScreenLimit;
        private Vector2 _maxScreenLimit;

        /// <summary>
        /// Screen's right upper corner in world position. 
        /// </summary>
        public Vector2 MaxScreenLimit{get => _maxScreenLimit;}
        
        /// <summary>
        /// Screen's left down corner in world position. 
        /// </summary>
        public Vector2 MinScreenLimit => _minScreenLimit;

        [HideInInspector]
        public bool IsRunning;
        
        private void Awake()
        {
            if (_instance == null) 
            {
                _instance = this;
            }
            else 
            {
                Debug.Log("Multiple GameManager present.", this);
            }
            
            Camera camera = Camera.main;
            ReferenceUtil.TestNullReferences(camera, "MainCamera", "GameManager");
            _maxScreenLimit = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            _minScreenLimit = camera.ScreenToWorldPoint(new Vector3(0, 0, 0));
            
            ReferenceUtil.TestNullReferences(_soundController, "SoundController", "GameManager");
            ReferenceUtil.TestNullReferences(_uiController, "UIController", "GameManager");
            ReferenceUtil.TestNullReferences(_timeController, "TimeController", "GameManager");
            ReferenceUtil.TestNullReferences(_cameraController, "CameraController", "GameManager");
            
            _scoreController = new ScoreController(_defaultHighscore, _defaultScorePerPlane);
            _timeLimit = _defaultTimeLimit;
            OnConfigLoaded(99, 60, 5);
            _uiController.UpdateScores(_scoreController.Score, _scoreController.Highscore);
        }
    
        private void OnDestroy() 
        {
            if (_instance == this) 
            {
                _instance = null;
            }
        }

        public void StartGame()
        {
            IsRunning = true;
            _uiController.EnableMainMenu(false);
            _scoreController.ResetScore();
            _uiController.UpdateScores(_scoreController.Score, _scoreController.Highscore);
            _timeController.StartTimer(_timeLimit, _uiController.UpdateTime, OnTimeIsUp);
        }

        /// <summary>
        /// Function to be called when a plane is destroyed.
        /// </summary>
        public void PlaneDestroyed()
        {
            _cameraController.Shake();
            _soundController.Explode();
            bool hasNewHighscore = _scoreController.AirplaneDestroyed();
            _uiController.UpdateScores(_scoreController.Score, _scoreController.Highscore);
            if (hasNewHighscore)
            {
                _soundController.HighScore();
                _uiController.HighScoreFX();
            }
        }

        /// <summary>
        /// Method to be called on a shot is fired.
        /// </summary>
        public void ShotFired()
        {
            _soundController.Shot();
        }

        private void OnConfigLoaded(int configHighscore, int configTimeLimit, int configScorePerPlane)
        {
            _scoreController.UpdateReferenceValues(configHighscore, configScorePerPlane);
            _uiController.UpdateScores(_scoreController.Score, _scoreController.Highscore);
            _timeLimit = configTimeLimit;
        }

        private void OnTimeIsUp()
        {
            IsRunning = false;
            _uiController.EnableMainMenu(true);
        }
    }
}
