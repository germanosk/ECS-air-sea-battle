using System.Collections;
using AirSeaBattle.Util;
using UnityEngine;

namespace AirSeaBattle.Controllers
{
    public class SoundController : MonoBehaviour
    {
    
#pragma warning disable 649
        [SerializeField]
        private AudioSource _explosionAudioPrefab;

        [SerializeField] 
        private int _explosionAmmount;
    
        [SerializeField] 
        private AudioSource _highscoreAudioPrefab;
    
        [SerializeField] 
        private int _highscoreAmmount;
        
        [SerializeField] 
        private AudioSource _shotAudioPrefab;
    
        [SerializeField] 
        private int _shotAmmount;
#pragma warning restore 649
    
        private AudioSource[] _explosionInstances;
        private AudioSource[] _highscoreInstances;
        private AudioSource[] _shotInstances;
    
    
        private void Awake()
        {
            InitializeAudioSourceArray(out _explosionInstances, _explosionAudioPrefab, _explosionAmmount, "Explosion AudioSource Prefab");
            InitializeAudioSourceArray(out _highscoreInstances, _highscoreAudioPrefab, _highscoreAmmount, "Highscore AudioSource Prefab");
            InitializeAudioSourceArray(out _shotInstances, _shotAudioPrefab, _shotAmmount, "Shot AudioSource Prefab");
        }

        private void InitializeAudioSourceArray(out AudioSource[] array, AudioSource prefab, int amount, string referenceName)
        {
            if (ReferenceUtil.TestNullReferences(prefab, referenceName, "SoundController"))
            {
                array = new AudioSource[0];
                return;
            }
        
            Transform localTransform = transform;
            array = new AudioSource[amount];
            for (int i = 0; i < amount; i++)
            {
                AudioSource source = Instantiate(prefab, localTransform);
                source.enabled = false;
                array[i] = source;
            }
        }

        public void Explode()
        {
            PlaySound(_explosionInstances);
        }

        public void HighScore()
        {
            PlaySound(_highscoreInstances);
        }

        public void Shot()
        {
            PlaySound(_shotInstances);
        }

        private void PlaySound(AudioSource[] instances)
        {
            for (int i = 0; i < instances.Length; i++)
            {
                if (!instances[i].enabled)
                {
                    instances[i].enabled = true;
                    instances[i].Play();
                    StartCoroutine(DisableRoutine(instances[i]));
                    return;
                }
            }
            // We could add more instances to AudioSource array here but it's probably overkill for the moment.
            Debug.LogError("All instances from this audio are already being played, try setup more instances.");
        }

        private IEnumerator DisableRoutine(AudioSource source)
        {
            yield return new WaitForSeconds(source.clip.length);
            source.enabled = false;
        }
    }
}
