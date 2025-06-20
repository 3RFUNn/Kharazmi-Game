using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SFXSystem.core;
using UnityEngine;

namespace SFXSystem
{
    [CreateAssetMenu]
    public class SoundSystemManager : SingletonScriptableObject<SoundSystemManager, ICreationMethodLocated>
    {
        public List<AudioData> AudioDatas = new List<AudioData>();
        public int audioSourceCount;
        private SoundBehaviour SoundBehaviour;
        private bool isInstantiated;
        protected override void OnSetup()
        {
            var go = new GameObject("Sound Manager");
            SoundBehaviour = go.AddComponent<SoundBehaviour>();
            SoundBehaviour.Setup();
            DontDestroyOnLoad(go);
        }

        public void PlaySFX(AudioClip clip)
        {
            SoundBehaviour.PlaySFX(clip);
        }

        public void PlaySFX(string ID, int delayMs = 0)
        {
            var data = AudioDatas.FirstOrDefault(a => a.ID == ID);
            if (data == null)
            {
                Debug.LogError($"SoundManager: No Data found with ID {ID}");
                return;
            }
            SoundBehaviour.PlaySFX(data, delayMs);
        }

        public void ChangeBGM(AudioData data, float fadeTime = 0)
        {
            SoundBehaviour.ChangeBGM(data,fadeTime);
        }

        public void ChangeBGM(string ID, float fadeTime = 0)
        {
            var data = AudioDatas.FirstOrDefault(a => a.ID == ID);
            if (data == null)
            {
                Debug.LogError($"SoundManager: No Data found with ID {ID}");
                return;
            }
            SoundBehaviour.ChangeBGM(data, fadeTime);
        }
        public void ChangeBGMVolumn(float v)
        {
            SoundBehaviour.ChangeBGMVolumn(v);
        }
        public void PlayBGM()
        {
            SoundBehaviour.PlayBGM();
        }

        public void StopBGM()
        {
            SoundBehaviour.StopBGM();
        }

        public void PauseBGM()
        {
            SoundBehaviour.PauseBGM();
        }

        public void ResumeBGM()
        {
            SoundBehaviour.ResumeBGM();
        }

        public void RestartBGM()
        {
            SoundBehaviour.ResumeBGM();
        }
        
    }
    [System.Serializable]
    public class AudioData
    {
        public string ID;
        public AudioClip clip;
        [Range(0,100)]
        public float volume=50;
        
        public void SetData(AudioSource source,float soundMultiplier=1)
        {
            source.clip = clip;
            source.volume = volume*soundMultiplier / 100f;
        }
        public enum AudioType
        {
            SFX,
            BGM
        }
    }
}