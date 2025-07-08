//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.Audio;

//public class VolumeSettings : MonoBehaviour
//{
//    [SerializeField] AudioMixer audioMixer;
//    [SerializeField] Slider masterVolumeSlider;
//    [SerializeField] Slider musicVolumeSlider;
//    [SerializeField] Slider sfxVolumeSlider;

//    private void Awake()
//    {
//        masterVolumeSlider.onValueChanged.AddListener(SetMaserVolume);
//        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
//        sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
//    }
//    private void OnDisable() 
//    { 
//        masterVolumeSlider.onValueChanged.RemoveAllListeners();
//        musicVolumeSlider.onValueChanged.RemoveAllListeners();
//        sfxVolumeSlider.onValueChanged.RemoveAllListeners();
//        SaveVolumeSettings(); 
//    }

//    private void Start()
//    {
//        masterVolumeSlider.value = PlayerPrefs.GetFloat(AudioSystem.Instance._masterVolumeKey, AudioSystem.Instance.defaultVolume);
//        musicVolumeSlider.value = PlayerPrefs.GetFloat(AudioSystem.Instance._musicVolumeKey, AudioSystem.Instance.defaultVolume);
//        sfxVolumeSlider.value = PlayerPrefs.GetFloat(AudioSystem.Instance._musicVolumeKey, AudioSystem.Instance.defaultVolume);
//    }

//    void SaveVolumeSettings()
//    {
//        PlayerPrefs.SetFloat(AudioSystem.Instance._masterVolumeKey, masterVolumeSlider.value);
//        PlayerPrefs.SetFloat(AudioSystem.Instance._musicVolumeKey, musicVolumeSlider.value);
//        PlayerPrefs.SetFloat(AudioSystem.Instance._musicVolumeKey, sfxVolumeSlider.value);
//    }

//    void SetMaserVolume(float value) { audioMixer.SetFloat(AudioSystem.Instance._masterVolumeKey, Mathf.Log10(value) * 20f); }
//    void SetMusicVolume(float value) { audioMixer.SetFloat(AudioSystem.Instance._musicVolumeKey, Mathf.Log10(value) * 20f); }
//    void SetSFXVolume(float value) { audioMixer.SetFloat(AudioSystem.Instance._musicVolumeKey, Mathf.Log10(value) * 20f); }
//}
