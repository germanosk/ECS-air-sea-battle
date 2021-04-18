using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AirSeaBattle.Controllers
{
    public class CameraController : MonoBehaviour
    {
        private Vector3 _localPosition;
        private Coroutine _shakeCoroutine;

        public float ShakeDuration = 1.0f;
        public float ShakeIntensity = 2.0f;
        
        
        private void Awake()
        {
            _localPosition = transform.localPosition;
        }

        public void Shake()
        {
            if (_shakeCoroutine != null)
            {
                StopCoroutine(_shakeCoroutine);
            }

            _shakeCoroutine = StartCoroutine(ShakeRoutine());
        }

        private IEnumerator ShakeRoutine()
        {
            float timer = Time.time + ShakeDuration;
            Vector3 position = _localPosition;
            while (timer >= Time.time)
            {
                position.x = Random.Range(-1f, 1f) * ShakeIntensity;
                position.y = Random.Range(-1f, 1f) * ShakeIntensity;
                transform.localPosition = position;
                yield return null;
            }

            transform.localPosition = _localPosition;
        }
    }
}