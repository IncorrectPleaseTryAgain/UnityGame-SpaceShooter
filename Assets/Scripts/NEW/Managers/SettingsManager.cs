using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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

    [Header("Controls")]
    InputActionRebindingExtensions.RebindingOperation rebindOperation;

    bool audioSettingsChanged = false;
    bool videoSettingsChanged = false;

    private void OnEnable()
    {
        ButtonLogic.OnButtonAction += HandleButtonAction;
        AddListeners();
    }
    private void OnDisable()
    {
        ButtonLogic.OnButtonAction -= HandleButtonAction;
        RemoveListeners();
    }
    private void Awake()
    {
        gameObject.SetActive(false); // Start with settings menu hidden
        LoadSettings();
        _resolutionDropdown.ClearOptions();
        _resolutionDropdown.AddOptions(VideoSystem.Instance.GetResolutionOptions());
    }

    /*
     * Button Logic
     */
    void HandleButtonAction(Actions action)
    {
        switch (action)
        {
            case Actions.OpenSettings:
                OpenSettingsHandler();
                break;
            case Actions.CloseSettings:
                CloseSettingsHandler();
                break;
            case Actions.SaveSettings:
                SaveSettingsHandler();
                break;
            case Actions.ResetSettingsAudio:
                ResetSettingsAudioHandler();
                break;
            case Actions.ResetSettingsControls:
                ResetSettingsControlsHandler();
                break;
            case Actions.ResetSettingsVideo:
                ResetSettingsVideoHandler();
                break;

        }
    }
    void OpenSettingsHandler() { LoadSettings(); }
    void CloseSettingsHandler() { ReloadSettings(); }
    void SaveSettingsHandler() { SaveSettings(); }
    void ResetSettingsAudioHandler()
    {
        AudioSystem.Instance.Reset();
        _audioMasterSlider.value = AudioSystem.Instance.defaultVolume;
        _audioMusicSlider.value = AudioSystem.Instance.defaultVolume;
        _audioSFXSlider.value = AudioSystem.Instance.defaultVolume;
    }
    void ResetSettingsVideoHandler()
    {
        VideoSystem.Instance.Reset();
        _fullscreenToggle.isOn = VideoSystem.Instance.defaultFullscreen;
        _resolutionDropdown.value = GetResolutionDropdownIndex(VideoSystem.Instance.defaultResolutionWidth, VideoSystem.Instance.defaultResolutionHeight);
    }
    void ResetSettingsControlsHandler() { }

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
    void SaveSettings()
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

        SaveSystem.Instance.SavePlayerData();
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
        // Audio
        _audioMasterSlider.onValueChanged.RemoveAllListeners();
        _audioMusicSlider.onValueChanged.RemoveAllListeners();
        _audioSFXSlider.onValueChanged.RemoveAllListeners();

        // Video
        _fullscreenToggle.onValueChanged.RemoveAllListeners();
        _resolutionDropdown.onValueChanged.RemoveAllListeners();
    }
}
