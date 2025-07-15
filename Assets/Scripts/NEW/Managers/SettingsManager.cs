using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System;

public class SettingsManager : MonoBehaviour
{
    const string _logTag = "SettingsManager";

    [Header("Audio")]
    [SerializeField] Slider _audioMasterSlider;
    [SerializeField] Slider _audioMusicSlider;
    [SerializeField] Slider _audioSFXSlider;

    [Header("Video")]
    [SerializeField] Toggle _fullscreenToggle;
    [SerializeField] TMP_Dropdown _resolutionDropdown;

    //[Header("Controls")]
    //InputActionRebindingExtensions.RebindingOperation rebindOperation;

    bool audioSettingsChanged = false;
    bool videoSettingsChanged = false;

    public static event Action<bool> OnSettingsIsActive;

    private void OnEnable()
    {
        LoadSettings();
    }
    private void OnDestroy()
    {
        RemoveListeners();
    }

    private void Awake()
    {
        AddListeners();
        gameObject.SetActive(false); // Start with settings menu hidden

        _resolutionDropdown.ClearOptions();
        _resolutionDropdown.AddOptions(VideoSystem.Instance.GetResolutionOptions());
    }

    /*
     * Button Handlers
     */
    public void ResetAudioSettingsHandler()
    {
        AudioSystem.Instance.Reset();
        _audioMasterSlider.value = AudioSystem.Instance.defaultVolume;
        _audioMusicSlider.value = AudioSystem.Instance.defaultVolume;
        _audioSFXSlider.value = AudioSystem.Instance.defaultVolume;
    }
    public void ResetVideoSettingsHandler()
    {
        VideoSystem.Instance.Reset();
        _fullscreenToggle.isOn = VideoSystem.Instance.defaultFullscreen;
        _resolutionDropdown.value = GetResolutionDropdownIndex(VideoSystem.Instance.defaultResolutionWidth, VideoSystem.Instance.defaultResolutionHeight);
    }

    public void OpenSettingsHandler()
    {
        LogSystem.Instance.Log("Opening settings...", LogType.Info, _logTag);
        LoadSettings();
        OnSettingsIsActive?.Invoke(true);
        gameObject.SetActive(true); // Show settings menu
    }
    public void CloseSettingsHandler()
    {
        LogSystem.Instance.Log("Closing settings...", LogType.Info, _logTag);
        ReloadSettings();
        OnSettingsIsActive?.Invoke(false);
        gameObject.SetActive(false); // Hide settings menu
    }
    public void SaveSettingsHandler()
    {
        LogSystem.Instance.Log("Saving settings...", LogType.Info, _logTag);

        // Save Audio Settings
        if (audioSettingsChanged)
        {
            audioSettingsChanged = false;
            AudioSystem.Instance.SaveAudioSettings();
        }
        // Save Video Settings
        if (videoSettingsChanged)
        {
            videoSettingsChanged = false;
            VideoSystem.Instance.SaveVideoSettings();
        }

        // Save Controls Settings
        InputSystem.Instance.SaveControls();

        OnSettingsIsActive?.Invoke(false);
        gameObject.SetActive(false); // Hide settings menu
    }


    /*
     * Settings Methods
     */
    void LoadSettings()
    {
        LogSystem.Instance.Log("Loading settings...", LogType.Info, _logTag);
        // Load Audio Settings
        LoadAudioSettings();
        // Load Video Settings
        LoadVideoSettings();
        // Load Controls Settings
        InputSystem.Instance.LoadControls();
    }
    void ReloadSettings()
    {
        LogSystem.Instance.Log("Reloading settings...", LogType.Info, _logTag);
        // Reload Audio Settings
        if (audioSettingsChanged)
        {
            audioSettingsChanged = false;
            AudioSystem.Instance.InitializeAudio();
            LoadAudioSettings();
        }

        LogSystem.Instance.Log("Video Settings Changed: " + videoSettingsChanged, LogType.Info, _logTag);

        // Reload Video Settings
        if (videoSettingsChanged)
        {
            videoSettingsChanged = false;
            VideoSystem.Instance.InitializeVideo();
            LoadVideoSettings();
        }
    }

    /*
     * Audio Settings Methods
     */
    void LoadAudioSettings()
    {
        _audioMasterSlider.value = AudioSystem.Instance.GetMixerMasterVolume();
        _audioMusicSlider.value = AudioSystem.Instance.GetMixerMusicVolume();
        _audioSFXSlider.value = AudioSystem.Instance.GetMixerSfxVolume();
    }
    void AudioMasterSliderValueChangedHandler(float value)
    {
        audioSettingsChanged = true;
        AudioSystem.Instance.SetMixerMasterVolume(value);
    }
    void AudioMusicSliderValueChangedHandler(float value)
    {
        audioSettingsChanged = true;
        AudioSystem.Instance.SetMixerMusicVolume(value);
    }
    void AudioSfxSliderValueChangedHandler(float value)
    {
        audioSettingsChanged = true;
        AudioSystem.Instance.SetMixerSfxVolume(value);
    }


    /*
     * Video Settings Methods
     */
    void LoadVideoSettings()
    {
        _fullscreenToggle.isOn = VideoSystem.Instance.fullscreen;
        _resolutionDropdown.value = GetResolutionDropdownIndex(VideoSystem.Instance.resolutionWidth, VideoSystem.Instance.resolutionHeight);
    }
    void FullscreeenToggleValueChangedHandler(bool value)
    {
        videoSettingsChanged = true;
        VideoSystem.Instance.SetFullscreen(value);
    }
    void ResolutionDropdownValueChangedHandler(int index)
    {
        videoSettingsChanged = true;
        VideoSystem.Instance.SetResolution(index);
    }
    int GetResolutionDropdownIndex(int width, int height)
    {
        return _resolutionDropdown.options.FindIndex(option => option.text == $"{width} x {height}");
    }


    void AddListeners()
    {
        MainMenuCanvasLogic.OnOpenSettings += OpenSettingsHandler;
        
        // Audio
        _audioMasterSlider.onValueChanged.AddListener(AudioMasterSliderValueChangedHandler);
        _audioMusicSlider.onValueChanged.AddListener(AudioMusicSliderValueChangedHandler);
        _audioSFXSlider.onValueChanged.AddListener(AudioSfxSliderValueChangedHandler);

        // Video
        _fullscreenToggle.onValueChanged.AddListener(FullscreeenToggleValueChangedHandler);
        _resolutionDropdown.onValueChanged.AddListener(ResolutionDropdownValueChangedHandler);
    }
    void RemoveListeners()
    {
        MainMenuCanvasLogic.OnOpenSettings -= OpenSettingsHandler;

        // Audio
        _audioMasterSlider.onValueChanged.RemoveAllListeners();
        _audioMusicSlider.onValueChanged.RemoveAllListeners();
        _audioSFXSlider.onValueChanged.RemoveAllListeners();

        // Video
        _fullscreenToggle.onValueChanged.RemoveAllListeners();
        _resolutionDropdown.onValueChanged.RemoveAllListeners();
    }
}
