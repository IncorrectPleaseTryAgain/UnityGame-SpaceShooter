using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsLogic : MonoBehaviour
{
    [Header("Sliders")]
    [SerializeField] private Slider SLDR_MasterAudio;
    [SerializeField] private Slider SLDR_MusicAudio;
    [SerializeField] private Slider SLDR_SfxAudio;

    [SerializeField] private Button BTN_Reset;
    private void OnDestroy()
    {
        SLDR_MasterAudio.onValueChanged.RemoveListener(OnMasterAudioSliderValueChangedHandler);
        SLDR_MusicAudio.onValueChanged.RemoveListener(OnMusicAudioSliderValueChangedHandler);
        SLDR_SfxAudio.onValueChanged.RemoveListener(OnSfxAudioSliderValueChangedHandler);
    }
    private void Awake()
    {
        SLDR_MasterAudio.onValueChanged.AddListener(OnMasterAudioSliderValueChangedHandler);
        SLDR_MusicAudio.onValueChanged.AddListener(OnMusicAudioSliderValueChangedHandler);
        SLDR_SfxAudio.onValueChanged.AddListener(OnSfxAudioSliderValueChangedHandler);
        BTN_Reset.onClick.AddListener(OnResetHandler);

        Load();
    }

    private void OnMasterAudioSliderValueChangedHandler(float value)
    {
        AudioSystem.Instance.SetMixerMasterVolume(value);
    }
    private void OnMusicAudioSliderValueChangedHandler(float value)
    {
        AudioSystem.Instance.SetMixerMusicVolume(value);
    }
    private void OnSfxAudioSliderValueChangedHandler(float value)
    {
        AudioSystem.Instance.SetMixerSfxVolume(value);
    }

    private void OnResetHandler()
    {
        AudioSystem.Instance.Reset();
        SLDR_MasterAudio.value = AudioSystem.Instance.defaultVolume;
        SLDR_MusicAudio.value = AudioSystem.Instance.defaultVolume;
        SLDR_SfxAudio.value = AudioSystem.Instance.defaultVolume;
        Load();
    }
    public void Cancel()
    {
        AudioSystem.Instance.InitializeAudio();
    }
    public void Load()
    {
        SLDR_MasterAudio.value = AudioSystem.Instance.GetMixerMasterVolume();
        SLDR_MusicAudio.value = AudioSystem.Instance.GetMixerMusicVolume();
        SLDR_SfxAudio.value = AudioSystem.Instance.GetMixerSfxVolume();
    }
    public void Save()
    {
        AudioSystem.Instance.SaveAudioSettings();
    }
}
