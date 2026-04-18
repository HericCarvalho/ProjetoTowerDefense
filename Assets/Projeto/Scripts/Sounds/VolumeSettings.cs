using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace AudioSystem
{
    public class VolumeSettings : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private AudioMixer mixer;
        [SerializeField] private string parameterName; 
        [SerializeField] private Slider slider;

        void Start()
        {
            
            float savedVolume = PlayerPrefs.GetFloat(parameterName, 1f);
            slider.value = savedVolume;

            
            UpdateVolume(savedVolume);

           
            slider.onValueChanged.AddListener(UpdateVolume);
        }

        public void UpdateVolume(float value)
        {
           
            float dB = Mathf.Log10(Mathf.Max(0.0001f, value)) * 20;
            mixer.SetFloat(parameterName, dB);

            
            PlayerPrefs.SetFloat(parameterName, value);
        }
    }
}