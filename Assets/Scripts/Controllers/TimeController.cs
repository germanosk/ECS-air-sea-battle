using System;
using UnityEngine;

namespace AirSeaBattle.Controllers
{
    public class TimeController : MonoBehaviour
    {
        private Action<int,float> _onTimeUpdate;
        private Action _onTimerIsUp;
        private float _timer;
        private float _duration;

        public void StartTimer(float timeInSeconds, Action<int, float> onTimeUpdate, Action onTimerIsUp)
        {
            _onTimeUpdate = onTimeUpdate;
            _onTimerIsUp = onTimerIsUp;
            _timer = Time.time+timeInSeconds;
            _duration = timeInSeconds;
        }

        private void Update()
        {
            float elapsed = _timer - Time.time;
            _onTimeUpdate?.Invoke((int)(elapsed), elapsed/_duration);
            if (Time.time > _timer)
            {
                _onTimerIsUp?.Invoke();
            }
        }
    }
}
